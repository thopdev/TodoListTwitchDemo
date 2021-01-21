using System;
using System.Linq;
using System.Text;
using Microsoft.Azure.Cosmos.Table;
using Todo.AzureFunctions.Constants;
using Todo.AzureFunctions.Entities;
using Todo.AzureFunctions.Factories.Factories;
using Todo.AzureFunctions.Services.Interfaces;
using Todo.Shared.Models;

namespace Todo.AzureFunctions.Services
{
    public class TodoListService : ITodoListService
    {
        private readonly CloudTable _cloudTable;

        public TodoListService(ICloudTableFactory factory)
        {
            _cloudTable = factory.CreateCloudTable(TableStorageConstants.TodoListTable);
        }

        public bool CanUserAccessList(ClientPrincipal principal, string listId)
        {
            var list = _cloudTable.ExecuteQuery(new TableQuery<TodoListEntity>().Where(TableQuery.GenerateFilterCondition("RowKey", QueryComparisons.Equal, listId))).FirstOrDefault();

            if (list == null)
            {
                throw new Exception("Could not find list");
            }

            return list.PartitionKey == principal.UserId;
        }
    }
}
