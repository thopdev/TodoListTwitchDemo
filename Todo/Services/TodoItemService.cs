using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using AutoMapper;
using Blazored.LocalStorage;
using Todo.Models;
using Todo.Services.Interfaces;
using Todo.Shared.Constants;
using Todo.Shared.Dto.TodoItems;

namespace Todo.Services
{
    public class TodoItemService : ITodoItemService
    {
        private const string StorageKey = "todoList";
        private const string StorageIdKey = "todoListId";

        private readonly IHttpService _httpService;
        private readonly IMapper _mapper;

        public TodoItemService( IHttpService httpService, IMapper mapper)
        {
            _httpService = httpService;
            _mapper = mapper;
        }

        public async Task AddTodoItemAsync(string listId, TodoItem todoItem)
        {
            var dto = new NewTodoItemDto
            {
                ListId = listId,
                Name = todoItem.Name,
                Priority = todoItem.Priority,
                Status = todoItem.Status
            };

            var result = await _httpService.PostAsync<string>("api/" + FunctionConstants.AddTodoItemFunction, dto);
            todoItem.Id = result;
        }

        public async Task UpdateItem(string listId, TodoItem todoItem)
        {
            var dto = _mapper.Map<UpdateTodoItemDto>(todoItem);
            dto.ListId = listId;
            await _httpService.PutVoidAsync("api/" + FunctionConstants.UpdateTodoItemFunction, dto);
        }

        public async Task DeleteItem(string listId, string id)
        {
            await _httpService.DeleteAsync($"api/{FunctionConstants.DeleteTodoItemFunction}/{listId}/{id}");
        }

        public async Task<IEnumerable<TodoItem>> GetList(string listId)
        {
            var dtos = await _httpService.GetAsync<List<TodoItemDto>>("api/" + FunctionConstants.GetItemsOfTodoListFunction + listId);
            var models = _mapper.Map<IEnumerable<TodoItem>>(dtos);
            return models;
        }
    }
}
