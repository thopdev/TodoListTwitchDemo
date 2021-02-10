using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Todo.AzureFunctions.Services.Interfaces;
using Todo.Shared.Constants;
using Todo.Shared.Dto.TodoItems;
using Todo.Shared.Dto.TodoLists;
using Todo.Shared.Enums;

namespace Todo.AzureFunctions.Functions.TodoItems
{
    public class GetItemsOfTodoListFunction
    {
        private readonly ITodoListService _todoListService;
        private readonly IMapper _mapper;
        private readonly IAuthService _authService;
        private readonly ITodoItemService _todoItemService;
        private readonly ITodoListMemberService _todoListMemberService;

        public GetItemsOfTodoListFunction(IMapper mapper, IAuthService authService, ITodoItemService todoItemService, ITodoListService todoListService, ITodoListMemberService todoListMemberService)
        {
            _mapper = mapper;
            _authService = authService;
            _todoItemService = todoItemService;
            _todoListService = todoListService;
            _todoListMemberService = todoListMemberService;
        }

        [FunctionName(FunctionConstants.TodoItem.Get)]
        public IActionResult Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = FunctionConstants.TodoItem.Get + "/{listId}")]
            HttpRequest req, string listId)
        {
            var user = _authService.GetClientPrincipalFromRequest(req);
            if (!_todoListService.CanUserAccessList(user, listId, ShareRole.View))
            {
                return new UnauthorizedResult();
            }

            if (string.IsNullOrEmpty(listId))
            {
                return new BadRequestErrorMessageResult("Id cannot be empty");
            }

            var todoList = _todoItemService.GetEntitiesForPartitionKey(listId).ToList();

            var result = new TodoListWithItemsDto{
                Items = _mapper.Map<List<TodoItemDto>>(todoList),
                Id = listId,
                ShareRole = _todoListMemberService.GetUserShareRole(user.UserId, listId) ?? ShareRole.Full
            };

            return new OkObjectResult(result);
        }
    }
}