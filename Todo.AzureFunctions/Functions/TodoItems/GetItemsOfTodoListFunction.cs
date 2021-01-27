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

namespace Todo.AzureFunctions.Functions.TodoItems
{
    public class GetItemsOfTodoListFunction
    {
        private readonly ITodoListService _todoListService;
        private readonly IMapper _mapper;
        private readonly IAuthService _authService;
        private readonly ITodoItemService _todoItemService;

        public GetItemsOfTodoListFunction(IMapper mapper, IAuthService authService, ITodoItemService todoItemService, ITodoListService todoListService)
        {
            _mapper = mapper;
            _authService = authService;
            _todoItemService = todoItemService;
            _todoListService = todoListService;
        }


        [FunctionName(FunctionConstants.TodoItem.Get)]
        public IActionResult Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = FunctionConstants.TodoItem.Get + "/{listId}")]
            HttpRequest req, string listId)
        {
            var user = _authService.GetClientPrincipalFromRequest(req);
            if (!_todoListService.CanUserAccessList(user, listId))
            {
                return new UnauthorizedResult();
            }

            if (string.IsNullOrEmpty(listId))
            {
                return new BadRequestErrorMessageResult("Id cannot be empty");
            }

            var todoList = _todoItemService.GetEntitiesForPartitionKey(listId).ToList();

            return new OkObjectResult(_mapper.Map<IEnumerable<TodoItemDto>>(todoList));
        }
    }
}