using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Todo.AzureFunctions.Entities;
using Todo.AzureFunctions.Factories.Factories;
using Todo.AzureFunctions.Services.Interfaces;
using Todo.Shared.Dto.TodoLists;
using Todo.Shared.Dto.TodoLists.Members;
using Todo.Shared.Models;

namespace Todo.AzureFunctions.Services
{
    public class TodoListService : CloudTableServiceBase<TodoListEntity>, ITodoListService
    {
        private readonly ITodoListMemberService _todoListMemberService;
        private readonly IMapper _mapper;

        public TodoListService(ICloudTableFactory factory, ITodoListMemberService todoListMemberService, IMapper mapper) : base(factory)
        {
            _todoListMemberService = todoListMemberService;
            _mapper = mapper;
        }

        public bool CanUserAccessList(ClientPrincipal principal, string listId)
        {
            var list = GetEntitiesForRowKey(listId).FirstOrDefault();

            if (list == null)
            {
                throw new Exception("Could not find list");
            }

            return list.PartitionKey == principal.UserId || _todoListMemberService.CanUserAccessList(listId, principal.UserId);
        }

        public IEnumerable<TodoListDto> GetListsForUser(string userId)
        {
            var todoLists = GetEntitiesForPartitionKey(userId).Concat(GetSharedListsForUser(userId));
            foreach (var todoList in todoLists)
            {
                var members = _todoListMemberService.GetEntitiesForPartitionKey(todoList.RowKey);
                var mappedTodoList = _mapper.Map<TodoListDto>(todoList);
                mappedTodoList.Members = _mapper.Map<List<TodoListMemberDto>>(members);
                yield return mappedTodoList;
            }
        }

        private IEnumerable<TodoListEntity> GetSharedListsForUser(string userId)
        {
            var lists = _todoListMemberService.GetEntitiesForPartitionKey(userId).Select(x => x.PartitionKey);
            foreach (var list in lists)
            {
               yield return GetEntitiesForRowKey(list).FirstOrDefault();
            }
        }
    }
}
