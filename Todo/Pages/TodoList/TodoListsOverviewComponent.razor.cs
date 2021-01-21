using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Todo.Services.Interfaces;

namespace Todo.Pages.TodoList
{
    public partial class TodoListsOverviewComponent
    {
        [Inject] public ITodoListService TodoListService { get; set; }

        public List<Models.TodoList> TodoLists { get; set; }

        protected override async Task OnInitializedAsync()
        {
            TodoLists = await TodoListService.GetAllLists();
        }

        public async Task NewTodoList(Models.TodoList todoList)
        {
            await TodoListService.Add(todoList);
        }

        public void Delete(Models.TodoList todoList)
        {
            TodoListService.Delete(todoList);
        }

        public void Update(Models.TodoList todoList)
        {
            TodoListService.Update(todoList);
        }

    }
}
