using System.Collections.Generic;
using System.Threading.Tasks;
using Todo.Models;

namespace Todo.Services.Interfaces
{
    public interface ITodoListService
    {
        Task<IEnumerable<TodoList>> GetAllLists();
        Task Add(TodoList todoList);
    }
}