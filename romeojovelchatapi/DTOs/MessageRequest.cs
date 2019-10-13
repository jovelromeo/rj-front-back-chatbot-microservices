using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace romeojovelchatapi.DTOs
{
    public class MessageRequest
    {
        public long UserId { get; set; }
        public string Email { get; set; }
        public string Message { get; set; }

        public MessageRequest(long userId, string email, string message)
        {
            UserId = userId;
            Email = email;
            Message = message;
        }
        
    }
}
