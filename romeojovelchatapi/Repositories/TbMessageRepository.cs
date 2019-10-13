using Microsoft.EntityFrameworkCore;
using romeojovelchatapi.Domain.Models;
using romeojovelchatapi.Domain.Persistence;
using romeojovelchatapi.Domain.Repositories;
using romeojovelchatapi.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace romeojovelchatapi.Repositories
{
    public class TbMessageRepository : ITbMessageRepository
    {
        private readonly AppDbContext _appDbContext;

        public TbMessageRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<ICollection<TbChatMessage>> GetLastMessages(int v)
        {
            return await (from x in _appDbContext.TbChatMessage orderby  x.TsCreated descending select x).Take(50).ToListAsync();
        }

        public async Task SaveAsync(TbChatMessage tb)
        {
            await _appDbContext.TbChatMessage.AddAsync(tb);
        }

        public async Task SaveChagesAsync()
        {
            await _appDbContext.SaveChangesAsync();
        }
    }
}
