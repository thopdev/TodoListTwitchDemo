using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos.Table;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Todo.AzureFunctions.Constants;
using Todo.AzureFunctions.Entities;
using Todo.AzureFunctions.Factories;
using Todo.AzureFunctions.Factories.Factories;
using Todo.AzureFunctions.Services.Interfaces;
using Todo.Shared.Constants;

namespace Todo.AzureFunctions.Functions.TodoItems
{
    public class DeleteTodoItemFunction
    {
        private readonly CloudTable _cloudTable;
        private readonly IAuthService _authService;

        private readonly ITodoListService _todoListService;

        public DeleteTodoItemFunction(ICloudTableFactory cloudTableFactory, IAuthService authService, ITodoListService todoListService)
        {
            _authService = authService;
            _todoListService = todoListService;
            _cloudTable = cloudTableFactory.CreateCloudTable(TableStorageConstants.TodoItemTable);
        }

        [FunctionName(FunctionConstants.DeleteTodoItemFunction)]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = FunctionConstants.DeleteTodoItemFunction + "/{listId}/{itemId}")]
            HttpRequest req,
            string listId, 
            string itemId)
        {
            var user = _authService.GetClientPrincipalFromRequest(req);

            if (!_todoListService.CanUserAccessList(user, listId))
            {
                return new UnauthorizedResult();
            }

            if (string.IsNullOrEmpty(itemId))
            {
                return new BadRequestObjectResult("Id or listId cannot be empty");
            }

            var result = await _cloudTable.ExecuteAsync(TableOperation.Retrieve<TodoItemEntity>(listId, itemId));
            if (result?.Result is TodoItemEntity entity)
            {
                _cloudTable.Execute(TableOperation.Delete(entity));
            }
            else
            {
                return new NotFoundResult();
            }
            
            return new NoContentResult();
        }
    }
}
