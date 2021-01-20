using System.Collections.Generic;
using System.Threading.Tasks;
using Todo.Models;

namespace Todo.Services.Interfaces
{
    public interface ITodoListService
    {
        Task<List<TodoList>> GetAllLists();
        Task Add(TodoList todoList);
        Task Update(TodoList todoList);
        Task Delete(TodoList todoList);
        event TodoListService.ToDoListChangedHandler OnTodoListChange;
    }
}