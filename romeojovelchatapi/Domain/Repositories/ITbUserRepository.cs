using romeojovelchatapi.Domain.Models;
using romeojovelchatapi.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace romeojovelchatapi.Domain.Repositories
{
    public interface ITbUserRepository
    {
        Task<bool> TbUserExists(string email);
        Task AddAsync(TbUser user);
        Task SaveChangesAsync();
        Task<TbUser> GetUserAsync(string email, string password);
    }
}
