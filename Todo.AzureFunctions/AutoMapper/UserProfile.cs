using AutoMapper;
using Todo.AzureFunctions.Entities;
using Todo.Shared.Dto;
using Todo.Shared.Models;

namespace Todo.AzureFunctions.AutoMapper
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<ClientPrincipal, UserEntity>();
            CreateMap<UserEntity, UserDto>();
        }
    }
}
