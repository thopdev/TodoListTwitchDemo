using System.Threading.Tasks;
using Todo.Shared.Models;

namespace Todo.Services.Interfaces
{
    public interface IAuthService
    {
        Task<ClientPrincipal> CheckAuthentication();
    }
}