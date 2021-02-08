using System.Collections.Generic;
using Todo.AzureFunctions.Entities;
using Todo.Shared.Dto.TodoLists;
using Todo.Shared.Enums;
using Todo.Shared.Models;

namespace Todo.AzureFunctions.Services.Interfaces
{
    public interface ITodoListService : ICloudTableServiceBase<TodoListEntity>
    {
        bool CanUserAccessList(ClientPrincipal principal, string listId, ShareRole requiredRole = ShareRole.View);

        IEnumerable<TodoListDto> GetListsForUser(string userId);
    }
}