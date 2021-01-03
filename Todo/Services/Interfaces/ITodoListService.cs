using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Todo.Models;

namespace Todo.Services.Interfaces
{
    public interface ITodoListService
    {
        Task AddTodoItemAsync(Guid todoListId, TodoItem todoItem);
        Task Save(IEnumerable<TodoItem> todoList);
        Task<IEnumerable<TodoItem>> GetList(Guid id);
        Task UpdateItem(Guid id, TodoItem todoItem);
        Task<Guid> GetListId();
    }
}