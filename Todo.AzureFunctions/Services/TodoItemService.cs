using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Azure.Cosmos.Table;
using Microsoft.Azure.Cosmos.Table.Protocol;
using Todo.AzureFunctions.Constants;
using Todo.AzureFunctions.Entities;
using Todo.AzureFunctions.Factories.Factories;
using Todo.AzureFunctions.Services.Interfaces;

namespace Todo.AzureFunctions.Services
{
    public class TodoItemService : ITodoItemService
    {
        private readonly CloudTable _todoItemCloudTable;

        public TodoItemService(ICloudTableFactory cloudTableFactory)
        {
            _todoItemCloudTable = cloudTableFactory.CreateCloudTable<TodoItemEntity>();
        }


        public IEnumerable<TodoItemEntity> GetAllForListId(string listId)
        {
            var query = _todoItemCloudTable.CreateQuery<TodoItemEntity>().Where(x => x.PartitionKey == listId);
            return query.ToList();
        }

        public void DeleteAllItemsWithListId(string listId)
        {
            var todoItems = GetAllForListId(listId);
            foreach (var todoItemEntity in todoItems)
            {
                _todoItemCloudTable.Execute(TableOperation.Delete(todoItemEntity));
            }
        }
    }
}
