using AutoMapper;
using Todo.Blazor.Models;
using Todo.Shared.Dto;
using Todo.Shared.Dto.TodoLists;

namespace Todo.Blazor.AutoMapper
{
    public class TodoListProfile : Profile
    {
        public TodoListProfile()
        {
            CreateMap<TodoListDto, TodoList>();

            CreateMap<UserDto, User>();

            CreateMap<TodoListShareDto, TodoListShare>();
            CreateMap<TodoListShare, TodoListShareDto > ();

        }
    }
}
