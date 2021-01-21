using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Todo.Models;
using Todo.Services.Interfaces;

namespace Todo.Shared
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

        private async void UpdateUserName()
        {
            var user = await AuthenticationStateProvider.GetAuthenticationStateAsync();
            UserName = user.User.Identity?.Name;
        }

        private void OnTodoListChange()
        {
            Console.WriteLine("Update" + TodoLists.GetHashCode());
            StateHasChanged();
        }
    }
}
