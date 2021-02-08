using System.Linq;
using System.Web.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Todo.AzureFunctions.Services.Interfaces;
using Todo.Shared.Constants;

namespace Todo.AzureFunctions.Functions.TodoLists
{
    public class GetTodoListFunction
    {
        private readonly IAuthService _authService;
        private readonly ITodoListService _todoListService;

        public GetTodoListFunction(IAuthService authService, ITodoListService todoListService)
        {
            _authService = authService;
            _todoListService = todoListService;

        }

        [FunctionName(FunctionConstants.TodoList.Get)]
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

            var todoLists = _todoListService.GetListsForUser(userId).ToList();

            return new OkObjectResult(todoLists);
        }
    }
}