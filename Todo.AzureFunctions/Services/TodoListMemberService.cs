using Todo.AzureFunctions.Entities;
using Todo.AzureFunctions.Extensions;
using Todo.AzureFunctions.Factories.Factories;
using Todo.AzureFunctions.Services.Interfaces;
using Todo.Shared.Enums;

namespace Todo.AzureFunctions.Services
{
    public class TodoListMemberService : CloudTableServiceBase<TodoListMemberEntity>, ITodoListMemberService
    {
        public TodoListMemberService(ICloudTableFactory cloudTableFactory) : base(cloudTableFactory)
        {
        }

        public bool CanUserAccessList(string listId, string userId, ShareRole requiredRole)
        {
            var result = CloudTable.GetTableByPartitionAndRowKey<TodoListMemberEntity>(listId, userId);

            if (result == null)
            {
                return false;
            }

            return result.Role >= requiredRole;
        }

        public ShareRole? GetUserShareRole(string userId, string listId)
        {
            var result = CloudTable.GetTableByPartitionAndRowKey<TodoListMemberEntity>(listId, userId);
            return result?.Role;
        }
    }
}
