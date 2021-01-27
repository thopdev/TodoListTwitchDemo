using Todo.AzureFunctions.Entities;
using Todo.AzureFunctions.Factories.Factories;
using Todo.AzureFunctions.Services.Interfaces;

namespace Todo.AzureFunctions.Services
{
    public class TodoItemService : CloudTableServiceBase<TodoItemEntity>, ITodoItemService
    {
        public TodoItemService(ICloudTableFactory cloudTableFactory) : base(cloudTableFactory)
        {
        }
    }
}
