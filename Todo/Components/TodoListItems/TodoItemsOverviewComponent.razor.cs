using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Todo.Blazor.Models;
using Todo.Blazor.Services.Interfaces;

namespace Todo.Blazor.Components.TodoListItems
{
    public partial class TodoItemsOverviewComponent
    {
        [Inject]
        public ITodoItemService TodoItemService { get; set; }
        [Inject]
        public ITodoListService TodoListService { get; set; }

        [Parameter] public bool DisplayChecked { get; set; }

        public TodoListWithItems TodoList { get; set; }

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

            TodoList = await TodoItemService.GetList(ListId);
        }


        public void AddTodoItemToList(TodoItem todoItem)
        {
            TodoItemService.AddTodoItemAsync(ListId, todoItem);
            TodoList.Items.Insert(0, todoItem);
        }

        public async Task RemoveFromTodoList(TodoItem item)
        {
            TodoList.Items.Remove(item);
            await TodoItemService.DeleteItem(ListId, item.Id);
        }

        public async Task Update(TodoItem item)
        {
            await TodoItemService.UpdateItem(ListId, item);
        }
    }
}
