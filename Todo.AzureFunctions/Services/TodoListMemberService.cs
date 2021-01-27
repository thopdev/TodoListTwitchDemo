using Todo.AzureFunctions.Entities;
using Todo.AzureFunctions.Extensions;
using Todo.AzureFunctions.Factories.Factories;
using Todo.AzureFunctions.Services.Interfaces;

namespace Todo.AzureFunctions.Services
{
    public class TodoListMemberService : CloudTableServiceBase<TodoListMemberEntity>, ITodoListMemberService
    {
        public TodoListMemberService(ICloudTableFactory cloudTableFactory) : base(cloudTableFactory)
        {
        }

        public bool CanUserAccessList(string listId, string userId)
        {
            return CloudTable.GetTableByPartitionAndRowKey<TodoListMemberEntity>(listId, userId) != null;
        }
    }
}
