using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Newtonsoft.Json;
using Todo.AzureFunctions.Entities;
using Todo.AzureFunctions.Services.Interfaces;
using Todo.Shared.Constants;
using Todo.Shared.Dto.TodoLists.Members;
using Todo.Shared.Enums;

namespace Todo.AzureFunctions.Functions.TodoLists.Members
{
    public class AddMemberToTodoListFunction
    {
        private readonly IAuthService _authService;
        private readonly ITodoListMemberService _listMemberService;
        private readonly ITodoListService _todoListService;

        public AddMemberToTodoListFunction(IAuthService authService, ITodoListMemberService listMemberService, ITodoListService todoListService)
        {
            _authService = authService;
            _listMemberService = listMemberService;
            _todoListService = todoListService;
        }

        [FunctionName(FunctionConstants.TodoList.Members.Add)]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = null)]
            HttpRequest req)
        {
            var user = _authService.GetClientPrincipalFromRequest(req);

            var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var data = JsonConvert.DeserializeObject<NewTodoListMemberDto>(requestBody);

            if (!_todoListService.CanUserAccessList(user, data.ListId, ShareRole.Full))
            {
                return new UnauthorizedResult();
            }

            var entity = new TodoListMemberEntity
            {
                PartitionKey = data.ListId,
                RowKey = data.UserId
            };

            _listMemberService.Insert(entity);

            return new OkResult();
        }
    }
}
