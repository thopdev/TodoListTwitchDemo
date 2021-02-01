using System.Collections.Generic;
using System.Threading.Tasks;
using Todo.Blazor.Models;

namespace Todo.Blazor.Services.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetUsers(string searchText);
    }
}