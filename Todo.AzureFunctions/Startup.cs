using System.Diagnostics;
using System.IO;
using AutoMapper;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Todo.AzureFunctions;
using Microsoft.Extensions.Configuration;
using Todo.AzureFunctions.Appsettings;
using Todo.AzureFunctions.Factories;
using Todo.AzureFunctions.Factories.Factories;
using Todo.AzureFunctions.Services;
using Todo.AzureFunctions.Services.Interfaces;

[assembly: FunctionsStartup(typeof(Startup))]

namespace Todo.AzureFunctions
{
    public class Startup : FunctionsStartup
    {

        public override void ConfigureAppConfiguration(IFunctionsConfigurationBuilder builder)
        {
            var context = builder.GetContext();


            builder.ConfigurationBuilder
                .AddJsonFile(Path.Combine(context.ApplicationRootPath, "appsettings.json"), optional: false, reloadOnChange: false)
                .AddJsonFile(Path.Combine(context.ApplicationRootPath, $"appsettings.{context.EnvironmentName}.json"), optional: true, reloadOnChange: false)
                .AddEnvironmentVariables();

        }



        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddOptions<StorageSettings>()
                .Configure<IConfiguration>((settings, configuration) => {
                    configuration.GetSection(StorageSettings.JsonKey).Bind(settings);
                });

            builder.Services.AddScoped<ICloudTableFactory, CloudTableFactory>();
            builder.Services.AddAutoMapper(GetType());

            if (Debugger.IsAttached)
            {
                builder.Services.AddScoped<IAuthService, DebugAuthService>();
            }
            else
            {
                builder.Services.AddScoped<IAuthService, AuthService>();
            }



        }
    }
}