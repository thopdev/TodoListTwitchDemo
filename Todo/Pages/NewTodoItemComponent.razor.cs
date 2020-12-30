using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Todo.Models;
using Todo.Models.Forms;

namespace Todo.Pages
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