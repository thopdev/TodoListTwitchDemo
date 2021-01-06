using AutoMapper;
using Todo.Models;
using Todo.Shared.Dto;

namespace Todo.AutoMapper
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