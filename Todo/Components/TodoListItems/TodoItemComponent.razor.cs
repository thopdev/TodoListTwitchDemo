using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Todo.Blazor.Models;
using Todo.Shared.Enums;

namespace Todo.Blazor.Components.TodoListItems
{
    public partial class TodoItemComponent
    {
        [Parameter] public TodoItem TodoItem { get; set; }
        [Parameter] public EventCallback<TodoItem> OnDelete { get; set; }

        [Parameter]
        public EventCallback<TodoItem> OnChange { get; set; }

        public string NewName { get; set; }

        public TodoItemPriority NewPriority { get; set; }

        public bool Editing { get; set; }

        public void Edit()
        {
            NewName = TodoItem.Name;
            NewPriority = TodoItem.Priority;
            Editing = true;
        }

        public async Task Save()
        {
            TodoItem.Name = NewName;
            TodoItem.Priority = NewPriority;
            Editing = false;
            await OnChange.InvokeAsync(TodoItem);
        }

        public async Task Delete()
        {
            await OnDelete.InvokeAsync(TodoItem);
        }

        public string GetPriorityColor()
        {
            return GetPriorityColor(TodoItem.Priority);
        }
        public string GetPriorityColor(TodoItemPriority priority)
        {
            return TodoItem.Priority switch
            {
                TodoItemPriority.Low => "bg-green-200",
                TodoItemPriority.Medium => "bg-orange-200",
                TodoItemPriority.High => "bg-red-200",
                _ => throw new ArgumentOutOfRangeException()
            };
        }

    }
}
