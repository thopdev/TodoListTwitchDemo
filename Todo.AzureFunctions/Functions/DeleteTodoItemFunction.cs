using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos.Table;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Todo.AzureFunctions.Entities;
using Todo.AzureFunctions.Factories;
using Todo.Shared.Constants;

namespace Todo.AzureFunctions.Functions
{
    public class DeleteTodoItemFunction
    {
        private readonly CloudTable _cloudTable;

        public DeleteTodoItemFunction(ICloudTableFactory cloudTableFactory)
        {
            _cloudTable = cloudTableFactory.CreateCloudTable();
        }

        [FunctionName(FunctionConstants.DeleteTodoItemFunction)]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = FunctionConstants.DeleteTodoItemFunction + "/{id}")]
            HttpRequest req, string id, ClaimsPrincipal claims)
        {
            if (!claims.Identity.IsAuthenticated)
            {
                return new UnauthorizedResult();
            }

            var listId = claims.Identity.Name;
            if (Debugger.IsAttached)
            {
                listId = "thopdev";
            }

            if (string.IsNullOrEmpty(id) && string.IsNullOrEmpty(listId))
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
