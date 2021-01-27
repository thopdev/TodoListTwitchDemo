using Todo.AzureFunctions.Entities;

namespace Todo.AzureFunctions.Services.Interfaces
{
    public interface ITodoListMemberService : ICloudTableServiceBase<TodoListMemberEntity>
    {
        bool CanUserAccessList(string listId, string userId);

    }
}