using romeojovelchatapi.Domain;
using romeojovelchatapi.DTOs;
using romeojovelchatapi.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace romeojovelchatapi.Services.Communication
{
    public class MessageResult:BaseResult
    {
        public MessageResponse Content { get; private set; }

        private MessageResult(bool success, string[] message, MessageResponse response) : base(success, message)
        {
            this.Content = response;
        }

        public MessageResult(MessageResponse response) : this(true, new string[] { }, response)
        {

        }

        public MessageResult(string[] message) : base(false, message)
        {
        }

        public MessageResult(string message) : base(false, new string[] { message })
        {
        }
    }
}
