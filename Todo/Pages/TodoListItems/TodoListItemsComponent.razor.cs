using System;
using System.Collections.ObjectModel;
using System.Linq;
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
        [Inject]
        public ITodoListService TodoListService { get; set; }

        [Parameter] public bool DisplayChecked { get; set; }

        public ObservableCollection<TodoItem> TodoItems { get; set; }




        [Parameter]
        public string ListId { get; set; }


        protected override async Task OnParametersSetAsync()
        {
            ListId ??= (await TodoListService.GetAllLists()).FirstOrDefault()?.Id;

            if (ListId == null)
            {
                var todoList = new Models.TodoList { Name = "First Todo List" };
                await TodoListService.Add(todoList);
                ListId = todoList.Id;
            }

            TodoItems = new ObservableCollection<TodoItem>(await TodoItemService.GetList(ListId));
        }


        public void AddTodoItemToList(TodoItem todoItem)
        {
            TodoItemService.AddTodoItemAsync(ListId, todoItem);
            TodoItems.Insert(0, todoItem);
        }

        public async Task RemoveFromTodoList(TodoItem item)
        {
            TodoItems.Remove(item);
            await TodoItemService.DeleteItem(ListId, item.Id);
        }

        public async Task Update(TodoItem item)
        {
            await TodoItemService.UpdateItem(ListId, item);
        }
    }
}
