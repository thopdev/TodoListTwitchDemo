using Microsoft.AspNetCore.Http;
using Todo.AzureFunctions.Models;

namespace Todo.AzureFunctions.Services.Interfaces
{
    public interface IAuthService
    {
        ClientPrincipal GetClientPrincipalFromRequest(HttpRequest req);
    }
}
