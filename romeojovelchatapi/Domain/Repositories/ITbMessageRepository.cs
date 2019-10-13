using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using romeojovelchatapi.Domain.Models;
using romeojovelchatapi.DTOs;

namespace romeojovelchatapi.Domain.Repositories
{
    public interface ITbMessageRepository
    {
        Task SaveAsync(TbChatMessage tb);
        Task SaveChagesAsync();
        Task<ICollection<TbChatMessage>> GetLastMessages(int v);
    }
}
