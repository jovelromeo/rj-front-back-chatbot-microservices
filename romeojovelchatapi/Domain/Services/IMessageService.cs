using romeojovelchatapi.DTOs;
using romeojovelchatapi.Services.Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace romeojovelchatapi.Domain.Services
{
    public interface IMessageService
    {
        Task<MessageResult> ProcessMessage(string message);
        Task<MessageResult> Save(MessageRequest messageRequest);
        Task<ICollection<ChatMessageResponse>> GetLastMessages();
    }
}
