using Microsoft.AspNetCore.Components;

namespace Todo.Blazor.Components.TodoList
{
    public partial class NewTodoListComponent
    {
        [Parameter] public EventCallback<Models.TodoList> OnCreate { get; set; }

        public Models.TodoList TodoList { get; set; }

        protected override void OnInitialized()
        {
            TodoList = new Models.TodoList();
        }

        public void OnSubmit()
        {
            OnCreate.InvokeAsync(TodoList);
            TodoList = new Models.TodoList();
        }

    }
}
