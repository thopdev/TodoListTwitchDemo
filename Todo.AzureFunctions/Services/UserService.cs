using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Todo.AzureFunctions.Entities;
using Todo.AzureFunctions.Factories.Factories;
using Todo.AzureFunctions.Services.Interfaces;
using Todo.Shared.Models;

namespace Todo.AzureFunctions.Services
{
    public class UserService : CloudTableServiceBase<UserEntity>, IUserService
    {
        private readonly IMapper _mapper;


        public UserService(ICloudTableFactory cloudTableFactory, IMapper mapper) : base(cloudTableFactory)
        {
            _mapper = mapper;
        }

        public void InsertIfNotExists(ClientPrincipal clientPrincipal)
        {
            InsertIfNotExists(_mapper.Map<UserEntity>(clientPrincipal));
        }

        public List<UserEntity> SearchUserDetails(string searchText)
        {
            return CloudTable.CreateQuery<UserEntity>().ToList().Where(u => u.UserDetails.ToLower().Contains(searchText)).Take(10).ToList();
        }

        public UserEntity GetByUserId(string userId)
        {
            return CloudTable.CreateQuery<UserEntity>().Where(x => x.RowKey == userId).ToList().FirstOrDefault();
        }

    }
}