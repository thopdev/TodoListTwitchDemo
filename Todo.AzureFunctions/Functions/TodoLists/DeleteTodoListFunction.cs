using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Todo.AzureFunctions.Services.Interfaces;
using Todo.Shared.Constants;

namespace Todo.AzureFunctions.Functions.TodoLists
{
    public class DeleteTodoListFunction
    {
        private readonly IAuthService _authService;
        private readonly ITodoListService _todoListService;
        private readonly ITodoItemService _itemService;

        public DeleteTodoListFunction(IAuthService authService, ITodoItemService itemService, ITodoListService todoListService)
        {
            _authService = authService;
            _itemService = itemService;
            _todoListService = todoListService;
        }

        [FunctionName(FunctionConstants.TodoList.Delete)]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = FunctionConstants.TodoList.Delete + "/{id}")]
            HttpRequest req, string id)
        {
            var user = _authService.GetClientPrincipalFromRequest(req);
            var listId = user.UserId;

            if (string.IsNullOrEmpty(id))
            {
                return new BadRequestObjectResult("Id or listId cannot be empty");
            }


            if (await _todoListService.DeleteAsync(listId, id))
            {
                _itemService.DeleteEntitiesWithPartitionKey(id);
                return new NoContentResult();

            }

            return new NotFoundResult();
        }
    }
}
