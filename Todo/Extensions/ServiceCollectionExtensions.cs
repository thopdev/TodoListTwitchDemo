using System;
using System.Net.Http;
using AutoMapper;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Plk.Blazor.DragDrop;
using Todo.Providers;
using Todo.Services;
using Todo.Services.Interfaces;

namespace Todo.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddOptions();
            services.AddAuthorizationCore();
            services.AddScoped<AuthenticationStateProvider, CustomAuthenticationProvider>();


            services.AddBlazoredLocalStorage();
            services.AddBlazorDragDrop();
            services.AddAutoMapper(typeof(Program));

            services.AddScoped<IHttpService, HttpService>();
            services.AddScoped<ITodoListService, TodoListService>();



            return services;
        }
    }
}