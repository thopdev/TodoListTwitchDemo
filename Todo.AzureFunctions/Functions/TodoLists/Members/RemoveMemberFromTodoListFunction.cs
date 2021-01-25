using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Todo.AzureFunctions.Services.Interfaces;
using Todo.Shared.Constants;

namespace Todo.AzureFunctions.Functions.TodoLists.Members
{
    public class RemoveMemberFromTodoListFunction
    {
        private readonly IAuthService _authService;
        private readonly ITodoListService _todoListService;
        private readonly ITodoListMemberService _todoListMemberService;

        public RemoveMemberFromTodoListFunction(IAuthService authService, ITodoListService todoListService, ITodoListMemberService todoListMemberService)
        {
            _authService = authService;
            _todoListService = todoListService;
            _todoListMemberService = todoListMemberService;
        }


        [FunctionName(FunctionConstants.TodoList.Members.Remove)]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "delete",
                Route = FunctionConstants.TodoList.Members.Remove + "/{listId}/{userId}")]
            HttpRequest req, string listId, string userId)
        {
            var user = _authService.GetClientPrincipalFromRequest(req);


            if (!_todoListService.CanUserAccessList(user, listId))
            {
                return new UnauthorizedResult();
            }

            if (!await _todoListMemberService.RemoveAsync(listId, userId))
            {
                return new NotFoundResult();
            }

            return new NoContentResult();


        }
    }
}
