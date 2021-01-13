using AutoMapper;
using Todo.Models;
using Todo.Shared.Dto.TodoLists;

namespace Todo.AutoMapper
{
    public class TodoListProfile : Profile
    {

        public TodoListProfile()
        {
            CreateMap<TodoListDto, TodoList>();
        }

    }
}
