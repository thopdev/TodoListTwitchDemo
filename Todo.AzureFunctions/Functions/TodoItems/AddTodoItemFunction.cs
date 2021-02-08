using System;
using System.Data;
using System.IO;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
    public class AddTodoItemFunction
    {
        private readonly IAuthService _authService;

        private readonly ITodoListService _todoListService;
        private readonly ITodoItemService _todoItemService;
        private readonly IMapper _mapper;

        public AddTodoItemFunction(IAuthService authService, ITodoListService todoItemService, ITodoItemService todoItemService1, IMapper mapper)
        {
            _authService = authService;
            _todoListService = todoItemService;
            _todoItemService = todoItemService1;
            _mapper = mapper;
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

            if (!_todoListService.CanUserAccessList(user, listId, ShareRole.Edit))
            {
                return new UnauthorizedResult();
            }

            _todoItemService.Insert(new TodoItemEntity
                {
                    PartitionKey = listId, RowKey = rowKey, Name = data.Name,
                    Priority = (int) data.Priority, Status = data.Status
                });

            return new OkObjectResult(rowKey);
        } 
    }
}
