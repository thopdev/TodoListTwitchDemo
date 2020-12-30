using System;
using System.Collections.Generic;
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

        public IList<TodoItem> TodoItems { get; set; } 

        public string NewTodoItemName { get; set; } = string.Empty;
        public bool IsInputEmpty => NewTodoItemName == string.Empty;


        protected override async Task OnInitializedAsync()
        {
            await Task.Delay(1000);
            TodoItems = (await TodoListService.Get()).ToList();
        }

        public void AddTodoItemToList()
        {
            TodoItems.Add(new TodoItem(){Name = NewTodoItemName});
            NewTodoItemName = string.Empty;
            Save();
        }

        public void RemoveFromTodoList(TodoItem item)
        {
            TodoItems.Remove(item);
            Save();

        }

        public void Save()
        {
            TodoListService.Save(TodoItems);
        }
    }
}
