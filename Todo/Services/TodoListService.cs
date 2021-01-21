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

        private  List<Models.TodoList> _todoLists;
        private Task<IEnumerable<TodoListDto>> _response;

        public delegate void ToDoListChangedHandler();

        public event ToDoListChangedHandler OnTodoListChange;

        public TodoListService(IHttpService httpService, IMapper mapper)
        {
            _httpService = httpService;
            _mapper = mapper;
        }


        public async Task<List<TodoList>> GetAllLists()
        {
            if (_response != null)
            {
                await _response;
            }
            else
            {
                _response =
                    _httpService.GetAsync<IEnumerable<TodoListDto>>(
                        "api/" + FunctionConstants.GetTodoListFunction);
            }

            return _todoLists ?? (_todoLists = _mapper.Map<IEnumerable<TodoList>>(await _response).ToList());
        }

        public async Task Add(TodoList todoList)
        {
            var list = await GetAllLists();
            list.Insert(0, todoList);
            var result = await _httpService.PostAsync<string>("api/" + FunctionConstants.AddTodoListFunction, todoList);
            todoList.Id = result;
            OnOnTodoListChange();
        }

        public async Task Update(TodoList todoList)
        {
            await _httpService.PutVoidAsync("api/" + FunctionConstants.UpdateTodoListFunction, todoList);
            OnOnTodoListChange();
        }

        public async Task Delete(TodoList todoItem)
        {
            var list = await GetAllLists();
            list.Remove(todoItem);
            await _httpService.DeleteAsync("api/" + FunctionConstants.DeleteTodoListFunction + "/" + todoItem.Id);
            OnOnTodoListChange();
        }

        protected virtual void OnOnTodoListChange()
        {
            OnTodoListChange?.Invoke();
        }
    }
}
