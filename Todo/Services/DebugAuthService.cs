using System.Threading.Tasks;
using Todo.Services.Interfaces;
using Todo.Shared.Models;

namespace Todo.Services
{
    public class DebugAuthService : IAuthService
    {
        public Task<ClientPrincipal> CheckAuthentication()
        {
            return Task.FromResult(new ClientPrincipal
            {
                IdentityProvider = "Twitter", UserDetails = "ThopDev", UserId = "ThopDev",
                UserRoles = new[] {"anonymous", "authenticated"}
            });
        }
    }
}