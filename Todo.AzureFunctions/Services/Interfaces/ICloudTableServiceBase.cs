using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Azure.Cosmos.Table;

namespace Todo.AzureFunctions.Services.Interfaces
{
    public interface ICloudTableServiceBase<T> where T : TableEntity, new()
    {
        IEnumerable<T> Get(int skip, int take);
        T GetEntityByPartitionAndRowKey(string partitionKey, string rowKey);
        IEnumerable<T> GetEntitiesForPartitionKey(string partitionKey);
        IEnumerable<T> GetEntitiesForRowKey(string rowKey);
        void DeleteEntitiesWithPartitionKey(string partitionKey);
        void Insert(T entity);
        void InsertIfNotExists(T entity);
        Task<bool> DeleteAsync(string partitionKey, string rowKey);
        bool DeleteByRowKey(string rowKey);
        TableResult Save(T entity);
    }
}