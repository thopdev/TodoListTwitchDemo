using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Todo.Models;

namespace Todo.Pages
{
    public partial class TodoItemComponent
    {
        [Parameter] public TodoItem TodoItem { get; set; }
        [Parameter]
        public EventCallback<TodoItem> OnDelete { get; set; }

        [Parameter]
        public EventCallback<TodoItem> OnChange { get; set; }

        public string NewName { get; set; }

        public bool Editing { get; set; }

        public void Edit()
        {
            NewName = TodoItem.Name;
            Editing = true;
        }

        public async Task Save()
        {
            TodoItem.Name = NewName;
            Editing = false;
            await OnChange.InvokeAsync(TodoItem);
        }

        public async Task Delete()
        {
            await OnDelete.InvokeAsync(TodoItem);
        }


    }
}
