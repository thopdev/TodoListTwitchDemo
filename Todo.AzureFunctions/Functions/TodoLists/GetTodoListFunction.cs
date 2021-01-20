using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos.Table;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Todo.AzureFunctions.Constants;
using Todo.AzureFunctions.Entities;
using Todo.AzureFunctions.Factories.Factories;
using Todo.AzureFunctions.Services.Interfaces;
using Todo.Shared.Constants;
using Todo.Shared.Dto.TodoLists;

namespace Todo.AzureFunctions.Functions.TodoLists
{
    public class GetTodoListFunction
    {
        private readonly CloudTable _cloudTable;
        private readonly IMapper _mapper;
        private readonly IAuthService _authService;
        private readonly ITodoListService _todoListService;

        public GetTodoListFunction(ICloudTableFactory cloudTableFactory, IMapper mapper, IAuthService authService, ITodoListService todoListService)
        {
            _mapper = mapper;
            _authService = authService;
            _todoListService = todoListService;
            _cloudTable = cloudTableFactory.CreateCloudTable(TableStorageConstants.TodoListTable);
        }


        [FunctionName(FunctionConstants.GetTodoListFunction)]
        public IActionResult Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get")]
            HttpRequest req)
        {
            var user = _authService.GetClientPrincipalFromRequest(req);

            var userId = user.UserId;

            if (string.IsNullOrEmpty(userId))
            {
                return new BadRequestErrorMessageResult("Id cannot be empty");
            }

            var query = new TableQuery<TodoListEntity>().Where(
                TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, userId));

            var todoList = _cloudTable.ExecuteQuery(query);

            return new OkObjectResult(_mapper.Map<IEnumerable<TodoListDto>>(todoList));
        }
    }
}