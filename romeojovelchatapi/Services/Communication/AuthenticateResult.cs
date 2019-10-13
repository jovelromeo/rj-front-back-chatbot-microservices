
using romeojovelchatapi.Domain;
using romeojovelchatapi.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace romeojovelchatapi.Services.Communication
{
    public class AuthenticateResult : BaseResult
    {
        public AuthenticateResponse  Content { get; private set; }
        private AuthenticateResult(bool success, string[] message, AuthenticateResponse registerDto) : base(success, message)
        {
            this.Content = registerDto;
        }

        public AuthenticateResult(AuthenticateResponse registerDto): this(true, new string[] { }, registerDto)
        {

        }

        public AuthenticateResult(string[] message) : base(false, message)
        {
        }

        public AuthenticateResult(string message) : base(false,new string[] { message })
        {
        }
    }
}
