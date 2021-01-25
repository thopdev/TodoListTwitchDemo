using System;
using System.Data;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Newtonsoft.Json;
using Todo.AzureFunctions.Constants;
using Todo.AzureFunctions.Entities;
using Todo.AzureFunctions.Factories;
using Todo.AzureFunctions.Factories.Factories;
using Todo.AzureFunctions.Services.Interfaces;
using Todo.Shared.Constants;
using Todo.Shared.Dto.TodoItems;
using CloudTable = Microsoft.Azure.Cosmos.Table.CloudTable;
using TableOperation = Microsoft.Azure.Cosmos.Table.TableOperation;

namespace Todo.AzureFunctions.Functions.TodoItems
{
    public class AddTodoItemFunction
    {
        private readonly CloudTable _cloudTable;
        private readonly IAuthService _authService;

        private readonly ITodoListService _todoListService;

        public AddTodoItemFunction(ICloudTableFactory cloudTableFactory, IAuthService authService, ITodoListService todoItemService)
        {
            _authService = authService;
            _todoListService = todoItemService;
            _cloudTable = cloudTableFactory.CreateCloudTable<TodoItemEntity>();
        }

        [FunctionName(FunctionConstants.TodoItem.Add)]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest req)
        {
            var user = _authService.GetClientPrincipalFromRequest(req);
            
            var rowKey = Guid.NewGuid().ToString();

            var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var data = JsonConvert.DeserializeObject<NewTodoItemDto>(requestBody);
            var listId = data.ListId;

            if (data.ListId == null)
            {
                return new BadRequestResult();
            }

            if (!_todoListService.CanUserAccessList(user, listId))
            {
                return new UnauthorizedResult();
            }

            _cloudTable.Execute(TableOperation.Insert(new TodoItemEntity
                {
                    PartitionKey = listId, RowKey = rowKey, Name = data.Name,
                    Priority = (int) data.Priority, Status = data.Status
                }));

                return new OkObjectResult(rowKey);
        } 
    }
}
