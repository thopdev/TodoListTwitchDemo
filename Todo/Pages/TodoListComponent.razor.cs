using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Todo.Models;
using Todo.Services.Interfaces;

namespace Todo.Pages
{
    public partial class TodoListComponent
    {
        [Inject]
        public ITodoListService TodoListService { get; set; }

        [Parameter] public bool DisplayChecked { get; set; }

        public ObservableCollection<TodoItem> TodoItems { get; set; }

        private Guid ListGuid { get; set; }


        protected override async Task OnInitializedAsync()
        {
            ListGuid = await TodoListService.GetListId();
            TodoItems =  new ObservableCollection<TodoItem>((await TodoListService.GetList()));
        }

        public void AddTodoItemToList(TodoItem todoItem)
        {
            TodoListService.AddTodoItemAsync(todoItem);
            TodoItems.Insert(0, todoItem);
        }

        public async Task RemoveFromTodoList(TodoItem item)
        {
            TodoItems.Remove(item);
            await TodoListService.DeleteItem(item.Id);
        }

        public async Task Update(TodoItem item)
        {
            await TodoListService.UpdateItem(item);
        }
    }
}
