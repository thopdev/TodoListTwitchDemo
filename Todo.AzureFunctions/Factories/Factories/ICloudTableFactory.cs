using Microsoft.Azure.Cosmos.Table;

namespace Todo.AzureFunctions.Factories.Factories
{
    public interface ICloudTableFactory
    {
        CloudTable CreateCloudTable(string tableName);
    }
}