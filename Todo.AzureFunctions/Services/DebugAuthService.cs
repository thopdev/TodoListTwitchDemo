using Microsoft.AspNetCore.Http;
using Todo.AzureFunctions.Services.Interfaces;
using Todo.Shared.Models;

namespace Todo.AzureFunctions.Services
{
    public class DebugAuthService : IAuthService
    {
        private readonly IUserService _userService;

        public DebugAuthService(IUserService userService)
        {
            _userService = userService;
        }

        public ClientPrincipal GetClientPrincipalFromRequest(HttpRequest req)
        {
            var principle =  new ClientPrincipal
            {
                IdentityProvider = "ThopDevsSuperSecretAuth",
                UserId = "ThopDev",
                UserDetails = "ThopDev"
            };
            _userService.InsertIfNotExists(principle);
            return principle;
        }
    }
}