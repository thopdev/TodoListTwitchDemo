using System.Collections.Generic;
using System.Threading.Tasks;
using Todo.AzureFunctions.Entities;

namespace Todo.AzureFunctions.Services.Interfaces
{
    public interface ITodoListMemberService
    {
        void Add(TodoListMemberEntity member);
        Task<bool> RemoveAsync(string listId, string userId);
        List<TodoListMemberEntity> GetForListId(string listId);
        List<TodoListMemberEntity> GetForUserId(string userId);
        bool CanUserAccessList(string listId, string userId);

    }
}