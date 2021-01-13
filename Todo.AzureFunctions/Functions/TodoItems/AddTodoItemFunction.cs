using System;
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

        public AddTodoItemFunction(ICloudTableFactory cloudTableFactory, IAuthService authService)
        {
            _authService = authService;
            _cloudTable = cloudTableFactory.CreateCloudTable(TableStorageConstants.TodoItemTable);
        }

        [FunctionName(FunctionConstants.AddTodoItemFunction)]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest req)
        {
            var user = _authService.GetClientPrincipalFromRequest(req);

            var listId = user.UserId;
            var rowKey = Guid.NewGuid().ToString();

            var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                var data = JsonConvert.DeserializeObject<NewTodoItemDto>(requestBody);
            

                _cloudTable.Execute(TableOperation.Insert(new TodoItemEntity
                {
                    PartitionKey = listId, RowKey = rowKey, Name = data.Name,
                    Priority = (int) data.Priority, Status = data.Status
                }));

                return new OkObjectResult(rowKey);

            } 
    }
}
