using Todo.AzureFunctions.Entities;
using Todo.Shared.Enums;

namespace Todo.AzureFunctions.Services.Interfaces
{
    public interface ITodoListMemberService : ICloudTableServiceBase<TodoListMemberEntity>
    {
        bool CanUserAccessList(string listId, string userId, ShareRole requiredRole);
        ShareRole? GetUserShareRole(string userId, string listId);
    }
}