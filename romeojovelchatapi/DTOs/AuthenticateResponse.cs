using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace romeojovelchatapi.DTOs
{
    public class AuthenticateResponse
    {
        public string Token { get; set; }
        public DateTime ExpirationTime { get; set; }
    }
}
