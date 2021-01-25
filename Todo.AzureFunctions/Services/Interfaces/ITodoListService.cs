using System.Collections.Generic;
using Todo.Shared.Dto.TodoLists;
using Todo.Shared.Models;

namespace Todo.AzureFunctions.Services.Interfaces
{
    public interface ITodoListService
    {
        bool CanUserAccessList(ClientPrincipal principal, string listId);

        IEnumerable<TodoListDto> GetListsForUser(string userId);
    }
}