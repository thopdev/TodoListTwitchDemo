using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Azure.Cosmos.Table;
using Todo.AzureFunctions.Entities;
using Todo.AzureFunctions.Extensions;
using Todo.AzureFunctions.Factories.Factories;
using Todo.AzureFunctions.Services.Interfaces;

namespace Todo.AzureFunctions.Services
{
    public class TodoListMemberService : ITodoListMemberService
    {
        private readonly CloudTable _cloudTable;

        public TodoListMemberService(ICloudTableFactory cloudTableFactory)
        {
            _cloudTable = cloudTableFactory.CreateCloudTable<TodoListMemberEntity>();
        }

        public void Add(TodoListMemberEntity member)
        {
            _cloudTable.Execute(TableOperation.Insert(member));
        }

        public async Task<bool> RemoveAsync(string listId, string userId)
        {
            return await _cloudTable.DeleteEntity<TodoListMemberEntity>(listId, userId);
        }

        public List<TodoListMemberEntity> GetForListId(string listId)
        {
            return _cloudTable.CreateQuery<TodoListMemberEntity>().Where(x => x.PartitionKey == listId).ToList();
        }

        public List<TodoListMemberEntity> GetForUserId(string userId)
        {
            return _cloudTable.CreateQuery<TodoListMemberEntity>().Where(x => x.RowKey == userId).ToList();
        }

        public bool CanUserAccessList(string listId, string userId)
        {
            return _cloudTable.GetTableByPartitionAndRowKey<TodoListMemberEntity>(listId, userId) != null;
        }
    }
}
