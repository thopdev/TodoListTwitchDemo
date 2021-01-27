using System.Threading.Tasks;
using Todo.Shared.Dto.TodoLists.Members;

namespace Todo.Blazor.Services.Interfaces
{
    public interface ITodoListMemberService
    {
        Task AddMemberToTodoList(NewTodoListMemberDto member);
        Task RemoveMemberFromTodoList(string listId, string userId);
    }
}