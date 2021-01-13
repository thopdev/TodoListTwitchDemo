using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Todo.Models;
using Todo.Services.Interfaces;
using Todo.Shared.Constants;
using Todo.Shared.Dto.TodoLists;

namespace Todo.Services
{
    public class TodoListService : ITodoListService
    {
        private readonly IHttpService _httpService;
        private readonly IMapper _mapper;

        public TodoListService(IHttpService httpService, IMapper mapper)
        {
            _httpService = httpService;
            _mapper = mapper;
        }


        public async Task<IEnumerable<TodoList>> GetAllLists()
        {
            var result = await _httpService.GetAsync<IEnumerable<TodoListDto>>("api/" + FunctionConstants.GetTodoListFunction);
            return _mapper.Map<IEnumerable<TodoList>>(result);
        }

        public async Task Add(TodoList todoList)
        {
            var result = await _httpService.PostAsync<string>("api/" + FunctionConstants.AddTodoListFunction, todoList);
            todoList.Id = result;
        }



    }
}
