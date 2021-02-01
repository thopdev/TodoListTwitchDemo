using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Todo.Blazor.Models;
using Todo.Blazor.Services.Interfaces;
using Todo.Shared.Constants;
using Todo.Shared.Dto;

namespace Todo.Blazor.Services
{
    public class UserService : IUserService
    {
        private readonly IHttpService _httpService;
        private readonly IMapper _mapper;

        public UserService(IHttpService httpService, IMapper mapper)
        {
            _httpService = httpService;
            _mapper = mapper;
        }

        public async Task<IEnumerable<User>> GetUsers(string searchText)
        {
            return _mapper.Map<IEnumerable<User>>(await _httpService.GetAsync<IEnumerable<UserDto>>("api/" + FunctionConstants.User.Get + $"?UserDetail={searchText}"));
        }
    }
}
