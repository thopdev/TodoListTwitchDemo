using System;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Todo.Extensions;
using Todo.Services;
using Todo.Services.Interfaces;

namespace Todo
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
#if Release
            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress ) });
            builder.Services.AddScoped<IAuthService, AuthService>();
#else
            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("http://localhost:7071") });
            builder.Services.AddScoped<IAuthService, DebugAuthService>();

#endif
            builder.Services.AddServices();

            await builder.Build().RunAsync();
        }
    }
}
