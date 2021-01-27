using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Todo.Blazor.Models;
using Todo.Blazor.Models.Forms;

namespace Todo.Blazor.Pages.TodoListItems
{
    public partial class NewTodoItemComponent
    {
        public NewTodoItemFormModel TodoItem { get; set; } = new NewTodoItemFormModel();

        [Parameter]
        public EventCallback<TodoItem> OnNewTodoItem { get; set; }

        public async Task OnSubmit()
        {
            await OnNewTodoItem.InvokeAsync(new TodoItem{Name = TodoItem.Name});
            TodoItem = new NewTodoItemFormModel();
        }

    }
}