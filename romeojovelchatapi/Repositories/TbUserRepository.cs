using Microsoft.EntityFrameworkCore;
using romeojovelchatapi.Domain.Models;
using romeojovelchatapi.Domain.Persistence;
using romeojovelchatapi.Domain.Repositories;
using romeojovelchatapi.DTOs;
using romeojovelchatapi.Services.Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace romeojovelchatapi.Repositories
{
    public class TbUserRepository : ITbUserRepository
    {
        private readonly AppDbContext _appDbContext;

        public TbUserRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task AddAsync(TbUser user)
        {
            await _appDbContext.TbUser.AddAsync(user);
        }

        public async Task<TbUser> GetUserAsync(string email, string password)
        {
            var user = await _appDbContext.TbUser.SingleOrDefaultAsync(x => string.Equals(x.DsEmail,email, StringComparison.OrdinalIgnoreCase));
            if (user==null || !BCrypt.Net.BCrypt.Verify(password, user.DsPassword))
            {
                return null;
            }
            user.DsPassword = string.Empty;
            return user;

        }

        public async Task SaveChangesAsync()
        {
            await _appDbContext.SaveChangesAsync();
        }

        public async Task<bool> TbUserExists(string email)
        {
            return await _appDbContext.TbUser.AnyAsync(e => e.DsEmail == email);
        }


    }
}
