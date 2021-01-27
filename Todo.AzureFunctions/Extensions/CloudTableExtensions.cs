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
    }
}
