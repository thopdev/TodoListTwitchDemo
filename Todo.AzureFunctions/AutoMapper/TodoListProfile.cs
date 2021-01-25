using AutoMapper;
using Todo.AzureFunctions.Entities;
using Todo.Shared.Dto.TodoLists;
using Todo.Shared.Dto.TodoLists.Members;

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
                .ForMember(dest => dest.RowKey, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.PartitionKey, opt => opt.MapFrom(src => src.OwnerId));


            CreateMap<TodoListMemberEntity, TodoListMemberDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.RowKey));
        }
    }
}