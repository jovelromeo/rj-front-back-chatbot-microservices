using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using romeojovelchatapi.Domain.Models;
using romeojovelchatapi.Domain.Persistence;
using romeojovelchatapi.Domain.Repositories;
using romeojovelchatapi.Domain.Services;
using romeojovelchatapi.DTOs;
using romeojovelchatapi.Helpers;
using romeojovelchatapi.Services.Communication;

namespace romeojovelchatapi.Services
{
    [Authorize]
    public class UserService : IUserService
    {
        private readonly ITbUserRepository _repository;
        private readonly IMapper _mapper;
        private readonly AppSettings _appSettings;

        public UserService(ITbUserRepository repository, IMapper mapper, IOptions<AppSettings> appSettings)
        {
            _repository = repository;
            _mapper = mapper;
            _appSettings = appSettings.Value;
        }


        public async Task<AuthenticateResult> Authenticate(AuthenticateRequest request)
        {
            var user = await _repository.GetUserAsync(request.Email, request.Password);

            // return null if user not found
            if (user == null)
                return null;
            user.DsPassword = string.Empty;
            // authentication successful so generate jwt token
            return GetToken(user);
        }

        public async Task<AuthenticateResult> RegisterAsync(RegisterRequest request)
        {
            await Task.Yield();
            var errors = new List<string>();
            //check for valid email
            if (!IsValidEmail(request.Email))
            {
                errors.Add("Email format is not valid.");
            }
            //there can be just 1 account per email: check email doesn't exists
            if (await _repository.TbUserExists(request.Email))
            {
                errors.Add("The email is already in use.");
            }
            if (request.Password.Length < 8)
            {
                errors.Add("The password must be at least 8 characters.");
            }
            if (errors.Count > 0)
            {
                return new AuthenticateResult(errors.ToArray());
            }
            var passwordHash = HashPassword(request.Password);
            request.Password = string.Empty;
            request.ConfirmPassword = string.Empty;
            var user = _mapper.Map<TbUser>(request);
            user.DsPassword = passwordHash;
            try
            {
                await _repository.AddAsync(user);
                await _repository.SaveChangesAsync();
                return GetToken(user);
            }
            catch (Exception e)
            {
                return new AuthenticateResult(e.Message);
            }
        }

        private string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        private AuthenticateResult GetToken(TbUser user)
        {
            //generate token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]{
                    new Claim(ClaimTypes.NameIdentifier,user.CdUser.ToString()),
                    new Claim(ClaimTypes.Name, user.DsEmail)
                }),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);
            
            return new AuthenticateResult(
                new AuthenticateResponse
                {
                    Token = tokenString,
                    ExpirationTime = DateTime.UtcNow.AddDays(7)
                });
        }
    }
}
