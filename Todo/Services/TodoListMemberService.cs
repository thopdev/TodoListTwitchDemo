using System;
using System.Text.Json;
using System.Threading.Tasks;
using AutoMapper;
using Todo.Blazor.Models;
using Todo.Blazor.Services.Interfaces;
using Todo.Shared.Constants;
using Todo.Shared.Dto.TodoItems;
using Todo.Shared.Dto.TodoLists;
using Todo.Shared.Dto.TodoLists.Members;

namespace Todo.Blazor.Services
{
    public class TodoListMemberService : ITodoListMemberService
    {
        private readonly IHttpService _httpService;
        private readonly IMapper _mapper;

        public TodoListMemberService(IHttpService httpService, IMapper mapper)
        {
            _httpService = httpService;
            _mapper = mapper;
        }

        public async Task AddMemberToTodoList(string listId, User member)
        {

            var dto = new NewTodoListMemberDto
            {
                ListId = listId,
                UserId = member.UserId
            };

           await _httpService.PutVoidAsync("api/" + FunctionConstants.TodoList.Members.Add, dto);
        }

        public async Task RemoveMemberFromTodoList(string listId, string userId)
        {
            await _httpService.DeleteAsync("api/" + FunctionConstants.TodoList.Members.Remove + $"/{listId}/{userId}");
        }

        public async Task UpdateMemberShare(string listId, TodoListShare share)
        {
            var shareDto = _mapper.Map<TodoListShareDto>(share);
            Console.WriteLine("?");
            await _httpService.PutVoidAsync("api/" + FunctionConstants.TodoList.Members.Update + "/" + listId, shareDto);
        }
    }
}
