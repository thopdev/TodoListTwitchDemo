using Microsoft.Azure.Cosmos.Table;

namespace Todo.AzureFunctions.Factories
{
    public interface ICloudTableFactory
    {
        CloudTable CreateCloudTable();
    }
}