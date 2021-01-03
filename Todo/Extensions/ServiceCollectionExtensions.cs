using System;
using System.Net.Http;
using AutoMapper;
using Blazored.LocalStorage;
using Microsoft.Extensions.DependencyInjection;
using Plk.Blazor.DragDrop;
using Todo.Services;
using Todo.Services.Interfaces;

namespace Todo.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddBlazoredLocalStorage();
            services.AddBlazorDragDrop();
            services.AddAutoMapper(typeof(Program));

            services.AddScoped<IHttpService, HttpService>();
            services.AddScoped<ITodoListService, TodoListService>();


            return services;
        }
    }
}