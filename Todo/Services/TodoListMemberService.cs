using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Todo.Services.Interfaces;
using Todo.Shared.Constants;
using Todo.Shared.Dto.TodoLists.Members;

namespace Todo.Services
{
    public class TodoListMemberService : ITodoListMemberService
    {
        private readonly IHttpService _httpService;

        public TodoListMemberService(IHttpService httpService)
        {
            _httpService = httpService;
        }

        public async Task AddMemberToTodoList(NewTodoListMemberDto member)
        {
           await _httpService.PutVoidAsync("api/" + FunctionConstants.TodoList.Members.Add, member);
        }

        public async Task RemoveMemberFromTodoList(string listId, string userId)
        {
            await _httpService.DeleteAsync("api/" + FunctionConstants.TodoList.Members.Remove + $"/{listId}/{userId}");
        }
    }
}
