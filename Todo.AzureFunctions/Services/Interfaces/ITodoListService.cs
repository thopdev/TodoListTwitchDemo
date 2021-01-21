using Todo.Shared.Models;

namespace Todo.AzureFunctions.Services.Interfaces
{
    public interface ITodoListService
    {
        bool CanUserAccessList(ClientPrincipal principal, string listId);
    }
}