using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Todo.Models;
using Todo.Services.Interfaces;

namespace Todo.Pages.TodoListItems
{
    public partial class TodoListItemsComponent
    {
        [Inject]
        public ITodoItemService TodoItemService { get; set; }

        [Parameter] public bool DisplayChecked { get; set; }

        public ObservableCollection<TodoItem> TodoItems { get; set; }

        private Guid ListGuid { get; set; }


        protected override async Task OnInitializedAsync()
        {
            ListGuid = await TodoItemService.GetListId();
            TodoItems =  new ObservableCollection<TodoItem>((await TodoItemService.GetList()));
        }

        public void AddTodoItemToList(TodoItem todoItem)
        {
            TodoItemService.AddTodoItemAsync(todoItem);
            TodoItems.Insert(0, todoItem);
        }

        public async Task RemoveFromTodoList(TodoItem item)
        {
            TodoItems.Remove(item);
            await TodoItemService.DeleteItem(item.Id);
        }

        public async Task Update(TodoItem item)
        {
            await TodoItemService.UpdateItem(item);
        }
    }
}
