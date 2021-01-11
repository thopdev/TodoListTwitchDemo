using System.Collections.Generic;
using System.Threading.Tasks;
using Todo.Services.Interfaces;
using Todo.Shared.Models;

namespace Todo.Services
{
    public class AuthService : IAuthService
    {
        private readonly IHttpService _httpService;

        public AuthService(IHttpService httpService)
        {
            _httpService = httpService;
        }

        public async Task<ClientPrincipal> CheckAuthentication()
        {
            return (await _httpService.GetAsync<AuthResponse>(".auth/me")).ClientPrincipal;
        }
    }
}
