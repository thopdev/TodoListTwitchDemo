using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Todo.Blazor.Models;
using Todo.Blazor.Services.Interfaces;

namespace Todo.Blazor.Shared
{
    public partial class NavMenu
    {
        [Inject] public AuthenticationStateProvider AuthenticationStateProvider { get; set; }
        [Inject] public ITodoListService TodoListService { get; set; }

        public List<TodoList> TodoLists { get; set; }

        protected override async Task OnInitializedAsync() { 
            TodoLists = await TodoListService.GetAllLists();
            UpdateUserName();
            AuthenticationStateProvider.AuthenticationStateChanged += (task) => UpdateUserName();
            TodoListService.OnTodoListChange += OnTodoListChange;

        }

        private string UserName { get; set; }
        private string UserId { get; set; }

        private async void UpdateUserName()
        {
            var user = await AuthenticationStateProvider.GetAuthenticationStateAsync();
            UserName = user.User.Identity?.Name;
            UserId = user.User.Claims?.FirstOrDefault(x => x.Type ==  ClaimTypes.Name)?.Value;
        }

        private void OnTodoListChange()
        {
            Console.WriteLine("Update" + TodoLists.GetHashCode());
            StateHasChanged();
        }
    }
}
