using Microsoft.Azure.Cosmos.Table;
using Todo.Shared.Enums;

namespace Todo.AzureFunctions.Entities
{
    public class TodoListEntity : TableEntity
    {
        public string Name { get; set; }
        public bool Status { get; set; }
        public int Priority { get; set; }
    }
}
