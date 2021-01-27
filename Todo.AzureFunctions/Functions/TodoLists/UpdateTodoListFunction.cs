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
using Todo.Shared.Dto.TodoLists;

namespace Todo.AzureFunctions.Functions.TodoLists
{
    public class UpdateTodoListFunction
    {
        private readonly IMapper _mapper;
        private readonly ITodoListService _todoListService;

        private readonly IAuthService _authService;

        public UpdateTodoListFunction(IMapper mapper, IAuthService authService, ITodoListService todoListService)
        {
            _mapper = mapper;
            _authService = authService;
            _todoListService = todoListService;
        }

        [FunctionName(FunctionConstants.TodoList.Update)]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = null)]
            HttpRequest req)
        {
            var user = _authService.GetClientPrincipalFromRequest(req);

            var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var data = JsonConvert.DeserializeObject<UpdateTodoListDto>(requestBody);

            _todoListService.CanUserAccessList(user, data.ListId);


            var entity = _mapper.Map<TodoListEntity>(data);

            _todoListService.Save(entity);

            return new OkResult();
        }
    }
}
