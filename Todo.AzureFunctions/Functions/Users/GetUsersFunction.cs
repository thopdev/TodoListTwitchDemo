using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Http;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Todo.AzureFunctions.Entities;
using Todo.AzureFunctions.Services.Interfaces;
using Todo.Shared.Constants;
using Todo.Shared.Dto;

namespace Todo.AzureFunctions.Functions.Users
{
    public class GetUsersFunction
    {
        private readonly IAuthService _authService;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public GetUsersFunction(IAuthService authService, IUserService userService, IMapper mapper)
        {
            _authService = authService;
            _userService = userService;
            _mapper = mapper;
        }

        [FunctionName(FunctionConstants.User.Get)]
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

            var users = 
                req.Query.TryGetValue("UserDetail", out var searchText) ? 
                    _userService.SearchUserDetails(searchText) : 
                    _userService.Get(0, 10);


            return new OkObjectResult(_mapper.Map<IEnumerable<UserDto>>(users));
        }
    }
}