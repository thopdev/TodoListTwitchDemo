using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Todo.Models;

namespace Todo.Services.Interfaces
{
    public interface ITodoItemService
    {
        Task AddTodoItemAsync(TodoItem todoItem);
        Task Save(IEnumerable<TodoItem> todoList);
        Task<IEnumerable<TodoItem>> GetList();
        Task UpdateItem(TodoItem todoItem);
        Task<Guid> GetListId();
        Task DeleteItem(string id);
    }
}