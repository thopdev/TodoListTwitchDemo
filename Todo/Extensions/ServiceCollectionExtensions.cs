using AutoMapper;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Plk.Blazor.DragDrop;
using Todo.Blazor.Factories;
using Todo.Blazor.Factories.Interfaces;
using Todo.Blazor.Providers;
using Todo.Blazor.Services;
using Todo.Blazor.Services.Interfaces;

namespace Todo.Blazor.Extensions
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
            services.AddScoped<ITodoItemService, TodoItemService>();
            services.AddScoped<ILoaderItemFactory, LoaderItemFactory>();
            services.AddScoped<ITodoListService, TodoListService>();
            services.AddScoped<ITodoListMemberService, TodoListMemberService>();


            return services;
        }
    }
}