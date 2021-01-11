using Microsoft.AspNetCore.Http;
using Todo.AzureFunctions.Models;
using Todo.AzureFunctions.Services.Interfaces;

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