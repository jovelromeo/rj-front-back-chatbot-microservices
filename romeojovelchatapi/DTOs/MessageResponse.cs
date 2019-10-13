using romeojovelchatapi.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace romeojovelchatapi.DTOs
{
    public class MessageResponse
    {
        public MessageType MessageType { get; set; }
        public string Message { get; set; }
        public string Command  { get; set; }
        public string Code { get; set; }
    }
}
