using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Todo.Blazor.Extensions;
using Todo.Blazor.Services;
using Todo.Blazor.Services.Interfaces;

namespace Todo.Blazor
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            // #if Release
#if RELEASE
            // builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress ) });
            // builder.Services.AddScoped<IAuthService, AuthService>();
#else
            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("http://localhost:7071") });
             builder.Services.AddScoped<IAuthService, DebugAuthService>();
#endif
            builder.Services.AddServices();

            await builder.Build().RunAsync();
        }
    }
}
