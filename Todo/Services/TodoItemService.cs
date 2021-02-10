using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using AutoMapper;
using Todo.Blazor.Models;
using Todo.Blazor.Services.Interfaces;
using Todo.Shared.Constants;
using Todo.Shared.Dto.TodoItems;
using Todo.Shared.Dto.TodoLists;

namespace Todo.Blazor.Services
{
    public class TodoItemService : ITodoItemService
    {
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

            var result = await _httpService.PostAsync<string>("api/" + FunctionConstants.TodoItem.Add, dto);
            todoItem.Id = result;
        }

        public async Task UpdateItem(string listId, TodoItem todoItem)
        {
            var dto = _mapper.Map<UpdateTodoItemDto>(todoItem);
            dto.ListId = listId;
            await _httpService.PutVoidAsync("api/" + FunctionConstants.TodoItem.Update, dto);
        }

        public async Task DeleteItem(string listId, string id)
        {
            await _httpService.DeleteAsync($"api/{FunctionConstants.TodoItem.Delete}/{listId}/{id}");
        }

        public async Task<TodoListWithItems> GetList(string listId)
        {
            var dtos = await _httpService.GetAsync<TodoListWithItemsDto>("api/" + FunctionConstants.TodoItem.Get + "/" + listId);
            var models = _mapper.Map<TodoListWithItems>(dtos);
            return models;
        }
    }
}
