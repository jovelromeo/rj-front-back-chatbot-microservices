using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using romeojovelchatapi.Domain.Models;
using romeojovelchatapi.Domain.Persistence;
using romeojovelchatapi.Domain.Services;
using romeojovelchatapi.DTOs;
using romeojovelchatapi.Extensions;
using romeojovelchatapi.Services.Communication;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace romeojovelchatapi.Controllers
{
    [Authorize]
    [Route("api/[controller]/[action]")]
    public class UserController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UserController(IUserService userService, IMapper mapper, AppDbContext context)
        {
            _context = context;
            _userService = userService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IList<TbUser>>> GetUsers()
        {
            return Ok(await _context.TbUser.ToListAsync());
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult<AuthenticateResult>> Authenticate([FromBody] AuthenticateRequest authenticationRequest)
        {
            var user = await _userService.Authenticate(authenticationRequest);

            if (user == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(user.Content);
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult<AuthenticateResponse>> RegisterAsync([FromBody] RegisterRequest register)
        {
            await Task.Yield();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.GetErrorMessages());
            }
            
            var result = await _userService.RegisterAsync(register);

            if (!result.Success)
                return BadRequest(result.Message);

            return Created("User registered succesful",result.Content);

        }



    }

}
