using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Azure.Cosmos.Table;
using Microsoft.Extensions.Options;
using Todo.AzureFunctions.Appsettings;
using Todo.AzureFunctions.Constants;

namespace Todo.AzureFunctions.Factories
{
    public class CloudTableFactory : ICloudTableFactory
    {
        private readonly StorageSettings _storageSettings;

        public CloudTableFactory(IOptions<StorageSettings> storageSettings)
        {
            _storageSettings = storageSettings.Value;
        }


        public CloudTable CreateCloudTable()
        {
            var storageAccount =
                CloudStorageAccount.Parse(_storageSettings.Account);

            // Create the table client.
            var tableClient = storageAccount.CreateCloudTableClient();

            var cloudTable = tableClient.GetTableReference(TableStorageConstants.TodoListTable);
            cloudTable.CreateIfNotExists();
            return cloudTable;
        }


    }
}
