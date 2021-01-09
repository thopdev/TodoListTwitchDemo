using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Web.Http;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos.Table;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Todo.AzureFunctions.Entities;
using Todo.AzureFunctions.Factories;
using Todo.Shared.Constants;
using Todo.Shared.Dto;

namespace Todo.AzureFunctions.Functions
{
    public class GetTodoListFunction
    {
        private readonly CloudTable _cloudTable;
        private readonly IMapper _mapper;

        public GetTodoListFunction(ICloudTableFactory cloudTableFactory, IMapper mapper)
        {
            _mapper = mapper;
            _cloudTable = cloudTableFactory.CreateCloudTable();
        }


        [FunctionName(FunctionConstants.GetTodoListFunction)]
        public IActionResult Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)]
            HttpRequest req, ClaimsPrincipal claims)
        {
            try
            {
                var header = StaticWebAppsAuth.Parse(req);
                return new OkObjectResult(header);
            }
            catch (Exception e)
            {
                return new OkObjectResult(e.Message);
            }

            if (!claims.Identity.IsAuthenticated)
            {
                return new UnauthorizedResult();
            }

            var listId = claims.Identity.Name;

            if (Debugger.IsAttached)
            {
                listId = "thopdev";
            }


            if (string.IsNullOrEmpty(listId))
            {
                return new BadRequestErrorMessageResult("Id cannot be empty");
            }

            var query = new TableQuery<TodoListEntity>().Where(
                TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, listId));

            var todoList = _cloudTable.ExecuteQuery(query);

            return new OkObjectResult(_mapper.Map<IEnumerable<TodoItemDto>>(todoList));
        }
    }
    public static class StaticWebAppsAuth
    {
        public class ClientPrincipal
        {
            public string IdentityProvider { get; set; }
            public string UserId { get; set; }
            public string UserDetails { get; set; }
            public IEnumerable<string> UserRoles { get; set; }
        }

        public static ClientPrincipal Parse(HttpRequest req)
        {
            var principal = new ClientPrincipal();

            if (req.Headers.TryGetValue("x-ms-client-principal", out var header))
            {
                var data = header[0];
                var decoded = Convert.FromBase64String(data);
                var json = Encoding.ASCII.GetString(decoded);
                principal = JsonSerializer.Deserialize<ClientPrincipal>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }

            
            principal.UserRoles = principal.UserRoles?.Except(new string[] { "anonymous" }, StringComparer.CurrentCultureIgnoreCase);
            return principal;

            // if (!principal.UserRoles?.Any() ?? true)
            // {
            //     return new ClaimsPrincipal();
            // }
            //
            // var identity = new ClaimsIdentity(principal.IdentityProvider);
            // identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, principal.UserId));
            // identity.AddClaim(new Claim(ClaimTypes.Name, principal.UserDetails));
            // identity.AddClaims(principal.UserRoles.Select(r => new Claim(ClaimTypes.Role, r)));
            //
            // return new ClaimsPrincipal(identity);
        }
    }
}