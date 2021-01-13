using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace Todo.Pages.TodoList
{
    public partial class TodoListComponent
    {
        public bool Editing { get; set; }

        [Parameter] public Models.TodoList TodoList { get; set; }

        [Parameter] public EventCallback<Models.TodoList> OnChange { get; set; }
        [Parameter] public EventCallback<Models.TodoList> OnDelete { get; set; }


        public async Task OnSave()
        {
            Editing = false;
            await OnChange.InvokeAsync(TodoList);
        }

        public void Edit()
        {
            Editing = true;
        }

        public async Task Delete()
        {
            await OnDelete.InvokeAsync(TodoList);
        }

    }
}
