using System;
using System.Linq;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Authorization;
using Todo.Services.Interfaces;

namespace Todo.Providers
{
    public class CustomAuthenticationProvider : AuthenticationStateProvider
    {
        private readonly IAuthService _authService;

        public CustomAuthenticationProvider(IAuthService authService)
        {
            _authService = authService;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {


            var principal = await _authService.CheckAuthentication();
            
            Console.WriteLine(JsonSerializer.Serialize(principal));
            
            var identity = new ClaimsIdentity(principal.IdentityProvider);
            Console.WriteLine("Provider");
            identity.AddClaim(new Claim(ClaimTypes.Name, principal.UserDetails));
            Console.WriteLine("name");
            identity.AddClaims(principal.UserRoles.Select(r => new Claim(ClaimTypes.Role, r)));
            Console.WriteLine("roles");

            var claimsPrincipal = new ClaimsPrincipal(identity);
            Console.WriteLine("claims");

            return new AuthenticationState(claimsPrincipal);
        }
    }
}
