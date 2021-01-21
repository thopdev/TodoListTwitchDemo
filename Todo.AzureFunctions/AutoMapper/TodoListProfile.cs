using AutoMapper;
using Todo.AzureFunctions.Entities;
using Todo.Shared.Dto.TodoLists;

namespace Todo.AzureFunctions.AutoMapper
{
    public class TodoListProfile : Profile
    {
        public TodoListProfile()
        {
            CreateMap<TodoListEntity, TodoListDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.RowKey));

            CreateMap<NewTodoListDto, TodoListEntity>();

            CreateMap<UpdateTodoListDto, TodoListEntity>()
                .ForMember(dest => dest.RowKey, opt => opt.MapFrom(src => src.Id));

        }
    }
}