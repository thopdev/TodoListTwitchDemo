using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Todo.Blazor.Models;
using Todo.Blazor.Services.Interfaces;

namespace Todo.Blazor.Components.Users
{
    public partial class UserTypeAheadComponent
    {
        [Inject] public IUserService UserService { get; set; }

        public IEnumerable<User> Users { get; set; }

        public User NewUser { get; set; }

        private async Task<IEnumerable<User>> SearchUsers(string searchText)
        {
            return await UserService.GetUsers(searchText);
        }


    }
}
