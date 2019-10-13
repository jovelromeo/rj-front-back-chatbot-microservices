using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using romeojovelchatapi.DTOs;

namespace romeojovelchatapi.Domain.Services
{
    public interface IChatBotService
    {
        Task<string> RequestMessageAsync(MessageResponse content);
    }
}
