using Microsoft.Azure.Cosmos.Table;

namespace Todo.AzureFunctions.Entities
{
    public class TodoItemEntity : TableEntity
    {
        public string Name { get; set; }
        public bool Status { get; set; }
        public int Priority { get; set; }
    }
}
