using AutoMapper;
using Todo.Models;
using Todo.Shared.Dto.TodoLists;
using Todo.Shared.Dto.TodoLists.Members;

namespace Todo.AutoMapper
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
