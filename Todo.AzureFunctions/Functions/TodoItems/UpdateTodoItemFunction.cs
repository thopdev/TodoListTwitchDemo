using System.IO;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos.Table;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Newtonsoft.Json;
using Todo.AzureFunctions.Constants;
using Todo.AzureFunctions.Entities;
using Todo.AzureFunctions.Factories.Factories;
using Todo.AzureFunctions.Services.Interfaces;
using Todo.Shared.Constants;
using Todo.Shared.Dto.TodoItems;

namespace Todo.AzureFunctions.Functions.TodoItems
{
    public class UpdateTodoItemFunction
    {
        private readonly CloudTable _cloudTable;
        private readonly IMapper _mapper;
        private readonly IAuthService _authService;
        private readonly ITodoListService _todoListService;

        public UpdateTodoItemFunction(ICloudTableFactory cloudTableFactory, IMapper mapper, IAuthService authService, ITodoListService todoListService)
        {
            _mapper = mapper;
            _authService = authService;
            _todoListService = todoListService;
            _cloudTable = cloudTableFactory.CreateCloudTable<TodoItemEntity>();
        }

        [FunctionName(FunctionConstants.TodoItem.Update)]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = null)]
            HttpRequest req)
        {
            var user = _authService.GetClientPrincipalFromRequest(req);


            var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var data = JsonConvert.DeserializeObject<UpdateTodoItemDto>(requestBody);
            var entity = _mapper.Map<TodoItemEntity>(data);

            var listId = data.ListId;
            if (_todoListService.CanUserAccessList(user, listId))
            {
                return new UnauthorizedResult();
            }

            entity.PartitionKey = listId;
            entity.ETag = "*";

            _cloudTable.Execute(TableOperation.Merge(entity));

            return new OkResult();
        }

    }
}
