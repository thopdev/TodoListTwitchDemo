using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.Cosmos.Table;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Todo.AzureFunctions.Constants;
using Todo.Shared.Constants;
using Todo.AzureFunctions.Entities;
using Todo.AzureFunctions.Factories;
using CloudTable = Microsoft.Azure.Cosmos.Table.CloudTable;
using CloudTableClient = Microsoft.Azure.Cosmos.Table.CloudTableClient;
using Todo.Shared.Dto;
using TableOperation = Microsoft.Azure.Cosmos.Table.TableOperation;

namespace Todo.AzureFunctions.Functions
{
    public class AddTodoItemFunction
    {

        private readonly CloudTable _cloudTable;

        public AddTodoItemFunction(ICloudTableFactory cloudTableFactory)
        {
            _cloudTable = cloudTableFactory.CreateCloudTable();
        }


        [FunctionName(FunctionConstants.AddTodoItemFunction)]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var data = JsonConvert.DeserializeObject<TodoItemDto>(requestBody);

            _cloudTable.Execute(TableOperation.Insert(new TodoListEntity{PartitionKey = Guid.NewGuid().ToString(), RowKey = Guid.NewGuid().ToString(), Name = data.Name, Priority = (int) data.Priority, Status = data.Status}));

            return new OkResult();
        }
    }
}
