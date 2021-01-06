using System.IO;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos.Table;
using Microsoft.Azure.Documents;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Newtonsoft.Json;
using Todo.AzureFunctions.Entities;
using Todo.AzureFunctions.Factories;
using Todo.Shared.Constants;
using Todo.Shared.Dto;

namespace Todo.AzureFunctions.Functions
{
    public class UpdateTodoItemFunction
    {
        private readonly CloudTable _cloudTable;
        private readonly IMapper _mapper;

        public UpdateTodoItemFunction(ICloudTableFactory cloudTableFactory, IMapper mapper)
        {
            _mapper = mapper;
            _cloudTable = cloudTableFactory.CreateCloudTable();
        }

        [FunctionName(FunctionConstants.UpdateTodoItemFunction)]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.User, "put", Route = null)]
            HttpRequest req, ClaimsPrincipal claimsPrincipal)
        {
            var listId = claimsPrincipal.Identity.Name;

            var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var data = JsonConvert.DeserializeObject<UpdateTodoItemDto>(requestBody);
            var entity = _mapper.Map<TodoListEntity>(data);
            entity.RowKey = listId;
            entity.ETag = "*";

            _cloudTable.Execute(TableOperation.Merge(entity));

            return new OkResult();
        }

    }
}
