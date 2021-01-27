using AutoMapper;
using Todo.Blazor.Models;
using Todo.Shared.Dto.TodoItems;

namespace Todo.Blazor.AutoMapper
{
    public class TodoItemProfile : Profile
    {
        public TodoItemProfile()
        {
            CreateMap<TodoItemDto, TodoItem>();
            CreateMap<TodoItem, UpdateTodoItemDto>();
        }
    }
}