using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Azure.Cosmos.Table;
using Todo.AzureFunctions.Entities;
using Todo.AzureFunctions.Factories.Factories;
using Todo.AzureFunctions.Services.Interfaces;

namespace Todo.AzureFunctions.Services
{
    public abstract class CloudTableServiceBase<T> : ICloudTableServiceBase<T> where T : TableEntity, new()
    {
        protected readonly CloudTable CloudTable;

        protected CloudTableServiceBase(ICloudTableFactory cloudTableFactory)
        {
            CloudTable = cloudTableFactory.CreateCloudTable<T>();
        }

        public IEnumerable<T> Get(int skip, int take)
        {
            return CloudTable.CreateQuery<T>().ToList().Take(10);
        }

        public T GetEntityByPartitionAndRowKey(string partitionKey, string rowKey)
        {
            return CloudTable.CreateQuery<T>()
                .Where(x => x.PartitionKey == partitionKey && x.RowKey == rowKey).ToList().FirstOrDefault();
        }

        public IEnumerable<T> GetEntitiesForPartitionKey(string partitionKey)
        {
            return CloudTable.CreateQuery<T>().Where(x => x.PartitionKey == partitionKey).ToList();
        }

        public IEnumerable<T> GetEntitiesForRowKey(string rowKey)
        {
            return CloudTable.CreateQuery<T>().Where(x => x.RowKey == rowKey).ToList();
        }

        public void DeleteEntitiesWithPartitionKey(string partitionKey)
        {
            var todoItems = GetEntitiesForPartitionKey(partitionKey);
            foreach (var todoItemEntity in todoItems)
            {
                CloudTable.Execute(TableOperation.Delete(todoItemEntity));
            }
        }

        public void Insert(T entity)
        {
            CloudTable.Execute(TableOperation.Insert(entity));
        }

        public void InsertIfNotExists(T entity)
        {
            try
            {
                CloudTable.Execute(TableOperation.Insert(entity));
            }
            catch (StorageException exception)  when (exception.Message == "Conflict")
            {

            }
        }

        public bool DeleteByRowKey(string rowKey)
        {
            var result = GetEntitiesForRowKey(rowKey).First();
            
            if (result is ITableEntity entity)
            {
                CloudTable.Execute(TableOperation.Delete(entity));
                return true;
            }

            return false;

        }

        public async Task<bool> DeleteAsync(string partitionKey, string rowKey)
        {

            var result = await CloudTable.ExecuteAsync(TableOperation.Retrieve<TodoListMemberEntity>(partitionKey, rowKey));
            if (result?.Result is ITableEntity entity)
            {
                CloudTable.Execute(TableOperation.Delete(entity));
                return true;
            }

            return false;
        }

        public TableResult Save(T entity)
        {
            entity.ETag = "*";

            return CloudTable.Execute(TableOperation.Merge(entity));
        }
    }
}
