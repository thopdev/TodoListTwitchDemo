using System.Collections.Generic;
using System.Threading.Tasks;
using Todo.Models;

namespace Todo.Services.Interfaces
{
    public interface ITodoListService
    {
        Task Save(IEnumerable<TodoItem> todoList);
        Task<IEnumerable<TodoItem>> Get();
    }
}