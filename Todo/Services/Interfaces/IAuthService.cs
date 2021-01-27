using System.Threading.Tasks;
using Todo.Shared.Models;

namespace Todo.Blazor.Services.Interfaces
{
    public interface IAuthService
    {
        Task<ClientPrincipal> CheckAuthentication();
    }
}