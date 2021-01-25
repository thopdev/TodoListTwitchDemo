using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos.Table;
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

namespace Todo.AzureFunctions.Functions.TodoLists
{
    public class AddTodoListFunction
    {
        private readonly CloudTable _cloudTable;
        private readonly IAuthService _authService;

        public AddTodoListFunction(ICloudTableFactory cloudTableFactory, IAuthService authService)
        {
            _authService = authService;
            _cloudTable = cloudTableFactory.CreateCloudTable<TodoListEntity>();
        }

        [FunctionName(FunctionConstants.TodoList.Add)]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest req)
        {
            var user = _authService.GetClientPrincipalFromRequest(req);

            var listId = user.UserId;
            var rowKey = Guid.NewGuid().ToString();

            var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var data = JsonConvert.DeserializeObject<NewTodoItemDto>(requestBody);

            _cloudTable.Execute(TableOperation.Insert(new TodoListEntity()
            {
                PartitionKey = listId,
                RowKey = rowKey,
                Name = data.Name,
            }));

            return new OkObjectResult(rowKey);

        }
    }


}
