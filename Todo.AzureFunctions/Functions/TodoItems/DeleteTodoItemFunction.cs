using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos.Table;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Todo.AzureFunctions.Entities;
using Todo.AzureFunctions.Factories.Factories;
using Todo.AzureFunctions.Services.Interfaces;
using Todo.Shared.Constants;
using Todo.Shared.Enums;

namespace Todo.AzureFunctions.Functions.TodoItems
{
    public class DeleteTodoItemFunction
    {
        private readonly IAuthService _authService;

        private readonly ITodoListService _todoListService;
        private readonly ITodoItemService _itemService;

        public DeleteTodoItemFunction(IAuthService authService, ITodoListService todoListService, ITodoItemService itemService)
        {
            _authService = authService;
            _todoListService = todoListService;
            _itemService = itemService;
        }

        [FunctionName(FunctionConstants.TodoItem.Delete)]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = FunctionConstants.TodoItem.Delete + "/{listId}/{itemId}")]
            HttpRequest req,
            string listId, 
            string itemId)
        {
            var user = _authService.GetClientPrincipalFromRequest(req);

            if (!_todoListService.CanUserAccessList(user, listId, ShareRole.Edit))
            {
                return new UnauthorizedResult();
            }

            if (string.IsNullOrEmpty(itemId))
            {
                return new BadRequestObjectResult("Id or listId cannot be empty");
            }


            if (! await _itemService.DeleteAsync(listId, itemId))
            {
                return new NotFoundResult();
            }
            
            return new NoContentResult();
        }
    }
}
