using Microsoft.AspNetCore.Http;
using Todo.AzureFunctions.Services.Interfaces;
using Todo.Shared.Models;

namespace Todo.AzureFunctions.Services
{
    public class DebugAuthService : IAuthService
    {
        public ClientPrincipal GetClientPrincipalFromRequest(HttpRequest req)
        {
            return new ClientPrincipal
            {
                UserId = "ThopDev"
            };
        }
    }
}