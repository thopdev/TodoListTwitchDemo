using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos.Table;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Todo.AzureFunctions.Constants;
using Todo.AzureFunctions.Entities;
using Todo.AzureFunctions.Factories.Factories;
using Todo.AzureFunctions.Services.Interfaces;
using Todo.Shared.Constants;

namespace Todo.AzureFunctions.Functions.TodoLists
{
    public class DeleteTodoListFunction
    {
        private readonly CloudTable _cloudTable;
        private readonly IAuthService _authService;
        private readonly ITodoItemService _itemService;

        public DeleteTodoListFunction(ICloudTableFactory cloudTableFactory, IAuthService authService, ITodoItemService itemService)
        {
            _authService = authService;
            _itemService = itemService;
            _cloudTable = cloudTableFactory.CreateCloudTable(TableStorageConstants.TodoListTable);
        }

        [FunctionName(FunctionConstants.DeleteTodoListFunction)]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = FunctionConstants.DeleteTodoListFunction + "/{id}")]
            HttpRequest req, string id, ClaimsPrincipal claims)
        {
            var user = _authService.GetClientPrincipalFromRequest(req);
            var listId = user.UserId;

            if (string.IsNullOrEmpty(id))
            {
                return new BadRequestObjectResult("Id or listId cannot be empty");
            }

            var result = await _cloudTable.ExecuteAsync(TableOperation.Retrieve<TodoListEntity>(listId, id));
            if (result?.Result is TodoListEntity entity)
            {
                _cloudTable.Execute(TableOperation.Delete(entity));
                _itemService.DeleteAllItemsWithListId(id);
            }
            else
            {
                return new NotFoundResult();
            }
            
            return new NoContentResult();
        }
    }
}
