using AutoMapper;
using Todo.AzureFunctions.Entities;
using Todo.Shared.Dto.TodoItems;

namespace Todo.AzureFunctions.AutoMapper
{
    public class TodoItemProfile : Profile
    {
        public TodoItemProfile()
        {
            CreateMap<UpdateTodoItemDto, TodoItemEntity>()
                .ForMember(dest => dest.RowKey, opt => opt.MapFrom(src => src.Id));
            
            CreateMap<TodoItemEntity, TodoItemDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.RowKey));
        }
    }
}
