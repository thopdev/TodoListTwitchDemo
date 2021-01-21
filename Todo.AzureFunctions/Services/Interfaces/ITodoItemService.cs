using System.Collections.Generic;
using Todo.AzureFunctions.Entities;

namespace Todo.AzureFunctions.Services.Interfaces
{
    public interface ITodoItemService
    {
        IEnumerable<TodoItemEntity> GetAllForListId(string listId);
        void DeleteAllItemsWithListId(string listId);
    }
}