using Microsoft.Azure.Cosmos.Table;
using Microsoft.Extensions.Options;
using Todo.AzureFunctions.Appsettings;
using Todo.AzureFunctions.Factories.Factories;

namespace Todo.AzureFunctions.Factories
{
    public class CloudTableFactory : ICloudTableFactory
    {
        private readonly StorageSettings _storageSettings;

        public CloudTableFactory(IOptions<StorageSettings> storageSettings)
        {
            _storageSettings = storageSettings.Value;
        }


        public CloudTable CreateCloudTable<T>() where T : TableEntity
        {
            return CreateCloudTable(typeof(T).Name);
        }


        public CloudTable CreateCloudTable(string tableName)
        {
            var storageAccount =
                CloudStorageAccount.Parse(_storageSettings.Account);

            // Create the table client.
            var tableClient = storageAccount.CreateCloudTableClient();

            var cloudTable = tableClient.GetTableReference(tableName);
            cloudTable.CreateIfNotExists();
            return cloudTable;
        }


    }
}
