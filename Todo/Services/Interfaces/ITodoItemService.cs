using System.Collections.Generic;
using System.Threading.Tasks;
using Todo.Blazor.Models;

namespace Todo.Blazor.Services.Interfaces
{
    public interface ITodoItemService
    {
        Task AddTodoItemAsync(string listId, TodoItem todoItem);
        Task<IEnumerable<TodoItem>> GetList(string listId);
        Task UpdateItem(string listId, TodoItem todoItem);
        Task DeleteItem(string listId, string id);
    }
}