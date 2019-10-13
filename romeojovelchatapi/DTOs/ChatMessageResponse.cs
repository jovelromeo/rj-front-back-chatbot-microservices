using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace romeojovelchatapi.DTOs
{
    public class ChatMessageResponse
    {
        public long? userId { get; set; }
        public string message { get; set; }
    }
}
