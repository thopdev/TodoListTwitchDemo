using System.Collections.Generic;
using System.Threading.Tasks;
using Blazored.LocalStorage;
using Todo.Models;
using Todo.Services.Interfaces;

namespace Todo.Services
{
    public class TodoListService : ITodoListService
    {
        private const string StorageKey = "todoList";
        private readonly ILocalStorageService _localStorageService;

        public TodoListService(ILocalStorageService localStorageService)
        {
            _localStorageService = localStorageService;
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
