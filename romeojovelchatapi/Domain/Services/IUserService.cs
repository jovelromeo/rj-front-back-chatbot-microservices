using romeojovelchatapi.DTOs;
using romeojovelchatapi.Services.Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace romeojovelchatapi.Domain.Services
{
    public interface IUserService
    {
        Task<AuthenticateResult> Authenticate(AuthenticateRequest authenticationRequest);
        Task<AuthenticateResult> RegisterAsync(RegisterRequest request);
    }
}
