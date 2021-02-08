using System.Threading.Tasks;
using Todo.Blazor.Models;

namespace Todo.Blazor.Services.Interfaces
{
    public interface ITodoListMemberService
    {
        Task AddMemberToTodoList(string listId, User member);
        Task RemoveMemberFromTodoList(string listId, string userId);
        Task UpdateMemberShare(string listId, TodoListShare share);
    }
}