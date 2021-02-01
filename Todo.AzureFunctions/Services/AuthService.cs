using System;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Todo.AzureFunctions.Services.Interfaces;
using Todo.Shared.Models;

namespace Todo.AzureFunctions.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserService _userService;

        public AuthService(IUserService userService)
        {
            _userService = userService;
        }

        public ClientPrincipal GetClientPrincipalFromRequest(HttpRequest req)
        {
            if (req.Headers.TryGetValue("x-ms-client-principal", out var header))
            {
                var data = header.First();
                var decoded = Convert.FromBase64String(data);
                var json = Encoding.ASCII.GetString(decoded);
                var clientPrincipal = JsonConvert.DeserializeObject<ClientPrincipal>(json);
                _userService.InsertIfNotExists(clientPrincipal);
                return clientPrincipal;
            }
            
            return new ClientPrincipal();
        }


    }
}