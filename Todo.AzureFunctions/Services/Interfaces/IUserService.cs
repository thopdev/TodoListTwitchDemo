using System.Collections.Generic;
using System.Linq;
using Todo.AzureFunctions.Entities;
using Todo.Shared.Models;

namespace Todo.AzureFunctions.Services.Interfaces
{
    public interface IUserService : ICloudTableServiceBase<UserEntity>
    {
        void InsertIfNotExists(ClientPrincipal clientPrincipal);

        List<UserEntity> SearchUserDetails(string searchText);
    }
}