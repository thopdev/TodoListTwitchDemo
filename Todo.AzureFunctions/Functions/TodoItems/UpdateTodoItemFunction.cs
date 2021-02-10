using System.IO;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos.Table;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Newtonsoft.Json;
using Todo.AzureFunctions.Entities;
using Todo.AzureFunctions.Services.Interfaces;
using Todo.Shared.Constants;
using Todo.Shared.Dto.TodoItems;
using Todo.Shared.Enums;

namespace Todo.AzureFunctions.Functions.TodoItems
{
    public class UpdateTodoItemFunction
    {
        private readonly IMapper _mapper;
        private readonly IAuthService _authService;
        private readonly ITodoListService _todoListService;
        private readonly ITodoItemService _todoItemService;

        public UpdateTodoItemFunction(IMapper mapper, IAuthService authService, ITodoListService todoListService, ITodoItemService todoItemService)
        {
            _mapper = mapper;
            _authService = authService;
            _todoListService = todoListService;
            _todoItemService = todoItemService;
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
            if (!_todoListService.CanUserAccessList(user, listId, ShareRole.Edit))
            {
                return new UnauthorizedResult();
            }

            entity.PartitionKey = listId;

            _todoItemService.Save(entity);

            return new OkResult();
        }
    }
}
