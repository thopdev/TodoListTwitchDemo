using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Azure.Cosmos.Table;

namespace Todo.AzureFunctions.Extensions
{
    public static class CloudTableExtensions
    {

        public static T GetTableByPartitionAndRowKey<T>(this CloudTable cloudTable, string partitionKey, string rowKey) where T : TableEntity, new()
        {
            return cloudTable.CreateQuery<T>()
                .Where(x => x.PartitionKey == partitionKey && x.RowKey == rowKey).ToList().FirstOrDefault();
        }

        public static TableResult SaveEntity<T>(this CloudTable cloudTable, T entity) where T : TableEntity, new()
        {
            entity.ETag = "*";

            return cloudTable.Execute(TableOperation.Merge(entity));
        }

        public static async Task<bool> DeleteEntity<T>(this CloudTable cloudTable, string partitionKey, string rowKey) where T : ITableEntity
        {
            var result = await cloudTable.ExecuteAsync(TableOperation.Retrieve<T>(partitionKey, rowKey));
            if (result?.Result is ITableEntity entity)
            {
                cloudTable.Execute(TableOperation.Delete(entity));
                return true;
            }

            return false;

        }

    }
}
