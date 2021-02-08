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
using Todo.Shared.Dto.TodoLists;
using Todo.Shared.Enums;

namespace Todo.AzureFunctions.Functions.TodoLists.Members
{
    public class UpdateMemberShareFunction
    {
        private readonly IAuthService _authService;
        private readonly ITodoListService _todoListService;
        private readonly ITodoListMemberService _todoListMemberService;

        public UpdateMemberShareFunction(IAuthService authService, ITodoListService todoListService, ITodoListMemberService todoListMemberService)
        {
            _authService = authService;
            _todoListService = todoListService;
            _todoListMemberService = todoListMemberService;
        }


        [FunctionName(FunctionConstants.TodoList.Members.Update)]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "put",
                Route = FunctionConstants.TodoList.Members.Update + "/{listId}")]
            HttpRequest req, string listId)
        {
            var user = _authService.GetClientPrincipalFromRequest(req);
            if (!_todoListService.CanUserAccessList(user, listId, ShareRole.Full))
            {
                return new UnauthorizedResult();
            }

            var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var data = JsonConvert.DeserializeObject<TodoListShareDto>(requestBody);

            var entity = new TodoListMemberEntity
            {
                ListId = listId,
                Role = data.Role,
                UserId = data.Member.UserId
            };

            _todoListMemberService.Save(entity);

            return new OkResult();
        }
    }
}