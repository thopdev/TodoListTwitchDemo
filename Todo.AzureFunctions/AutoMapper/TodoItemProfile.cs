using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Todo.AzureFunctions.Entities;
using Todo.Shared.Dto;

namespace Todo.AzureFunctions.AutoMapper
{
    public class TodoItemProfile : Profile
    {
        public TodoItemProfile()
        {
            CreateMap<UpdateTodoItemDto, TodoListEntity>()
                .ForMember(dest => dest.PartitionKey, opt => opt.MapFrom(src => src.ListId))
                .ForMember(dest => dest.RowKey, opt => opt.MapFrom(src => src.Id));
            CreateMap<TodoListEntity, TodoItemDto>().ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.RowKey));
        }


    }
}
