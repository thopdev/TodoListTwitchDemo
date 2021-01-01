using System.Collections.Generic;
using System.Threading.Tasks;
using Blazored.LocalStorage;
using Todo.Models;
using Todo.Services.Interfaces;
using Todo.Shared.Constants;
using Todo.Shared.Dto;

namespace Todo.Services
{
    public class TodoListService : ITodoListService
    {
        private const string StorageKey = "todoList";
        private readonly ILocalStorageService _localStorageService;
        private readonly IHttpService _httpService;

        public TodoListService(ILocalStorageService localStorageService, IHttpService httpService)
        {
            _localStorageService = localStorageService;
            _httpService = httpService;
        }


        public async Task AddTodoItemAsync(TodoItem todoItem)
        {
            var dto = new TodoItemDto
            {
                Name = todoItem.Name,
                Priority = todoItem.Priority,
                Status = todoItem.Status
            };

            await _httpService.PostVoidAsync(FunctionConstants.AddTodoItemFunction, dto);
        }

        public async Task Save(IEnumerable<TodoItem> todoList)
        {
            await _localStorageService.SetItemAsync(StorageKey, todoList);
        }

        public async Task<IEnumerable<TodoItem>> Get()
        {
            if (await _localStorageService.ContainKeyAsync(StorageKey))
            {
                return await _localStorageService.GetItemAsync<IEnumerable<TodoItem>>(StorageKey);

            }

            return new List<TodoItem>{new TodoItem{Name = "Fill up your todo list!"}};
        }


    }
}
