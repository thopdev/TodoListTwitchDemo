using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos.Table;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Todo.AzureFunctions.Entities;
using Todo.AzureFunctions.Factories;
using Todo.AzureFunctions.Services.Interfaces;
using Todo.Shared.Constants;

namespace Todo.AzureFunctions.Functions
{
    public class DeleteTodoItemFunction
    {
        private readonly CloudTable _cloudTable;
        private readonly IAuthService _authService;

        public DeleteTodoItemFunction(ICloudTableFactory cloudTableFactory, IAuthService authService)
        {
            _authService = authService;
            _cloudTable = cloudTableFactory.CreateCloudTable();
        }

        [FunctionName(FunctionConstants.DeleteTodoItemFunction)]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = FunctionConstants.DeleteTodoItemFunction + "/{id}")]
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
            }
            else
            {
                return new NotFoundResult();
            }
            
            return new NoContentResult();
        }
    }
}
