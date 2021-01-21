using Microsoft.Azure.Cosmos.Table;

namespace Todo.AzureFunctions.Entities
{
    public class TodoListEntity : TableEntity
    {
        public string Name { get; set; }
    }
}
