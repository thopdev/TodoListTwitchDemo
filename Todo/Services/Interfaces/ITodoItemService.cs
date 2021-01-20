using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Todo.Models;

namespace Todo.Services.Interfaces
{
    public interface ITodoItemService
    {
        Task AddTodoItemAsync(string listId, TodoItem todoItem);
        Task<IEnumerable<TodoItem>> GetList(string listId);
        Task UpdateItem(string listId, TodoItem todoItem);
        Task DeleteItem(string listId, string id);
    }
}