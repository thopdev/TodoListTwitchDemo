using AutoMapper;
using Todo.Blazor.Models;
using Todo.Shared.Dto.TodoLists;
using Todo.Shared.Dto.TodoLists.Members;

namespace Todo.Blazor.AutoMapper
{
    public class TodoListProfile : Profile
    {

        public TodoListProfile()
        {
            CreateMap<TodoListDto, TodoList>();

            CreateMap<TodoListMemberDto, TodoListMember>();
        }

    }
}
