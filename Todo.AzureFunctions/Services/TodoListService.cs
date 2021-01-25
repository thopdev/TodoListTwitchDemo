using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using AutoMapper;
using Microsoft.Azure.Cosmos.Table;
using Todo.AzureFunctions.Entities;
using Todo.AzureFunctions.Factories.Factories;
using Todo.AzureFunctions.Services.Interfaces;
using Todo.Shared.Dto.TodoLists;
using Todo.Shared.Dto.TodoLists.Members;
using Todo.Shared.Models;

namespace Todo.AzureFunctions.Services
{
    public class TodoListService : ITodoListService
    {
        private readonly CloudTable _cloudTable;
        private readonly ITodoListMemberService _todoListMemberService;
        private readonly IMapper _mapper;

        public TodoListService(ICloudTableFactory factory, ITodoListMemberService todoListMemberService, IMapper mapper)
        {
            _todoListMemberService = todoListMemberService;
            _mapper = mapper;
            _cloudTable = factory.CreateCloudTable<TodoListEntity>();
        }

        public bool CanUserAccessList(ClientPrincipal principal, string listId)
        {
            var list = _cloudTable
                .ExecuteQuery(new TableQuery<TodoListEntity>().Where(
                    TableQuery.GenerateFilterCondition("RowKey", QueryComparisons.Equal, listId))).FirstOrDefault();

            if (list == null)
            {
                throw new Exception("Could not find list");
            }

            return list.PartitionKey == principal.UserId || _todoListMemberService.CanUserAccessList(listId, principal.UserId);
        }

        public IEnumerable<TodoListDto> GetListsForUser(string userId)
        {
            var todoLists = GetListsForUserWithoutMembers(userId).Concat(GetSharedListsForUser(userId));
            foreach (var todoList in todoLists)
            {
                var members = _todoListMemberService.GetForListId(todoList.RowKey);
                var mappedTodoList = _mapper.Map<TodoListDto>(todoList);
                mappedTodoList.Members = _mapper.Map<List<TodoListMemberDto>>(members);
                yield return mappedTodoList;
            }
        }

        private IEnumerable<TodoListEntity> GetListsForUserWithoutMembers(string userId)
        {
            return _cloudTable.CreateQuery<TodoListEntity>().Where(x => x.PartitionKey == userId).ToList();
        }

        private IEnumerable<TodoListEntity> GetSharedListsForUser(string userId)
        {
            var lists = _todoListMemberService.GetForUserId(userId).Select(x => x.PartitionKey);
            foreach (var list in lists)
            {
               yield return _cloudTable.CreateQuery<TodoListEntity>().Where(x => x.RowKey == list).ToList().FirstOrDefault();
            }

        }
    }

}
