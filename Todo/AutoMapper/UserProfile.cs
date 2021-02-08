using AutoMapper;
using Todo.Blazor.Models;
using Todo.Shared.Dto;

namespace Todo.Blazor.AutoMapper
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserDto, User>().ReverseMap();
        }
    }
}
