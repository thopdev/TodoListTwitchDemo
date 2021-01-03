﻿using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text.Json;
using System.Threading.Tasks;
using AutoMapper;
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
        private const string StorageIdKey = "todoListId";

        private readonly ILocalStorageService _localStorageService;
        private readonly IHttpService _httpService;
        private readonly IMapper _mapper;

        public TodoListService(ILocalStorageService localStorageService, IHttpService httpService, IMapper mapper)
        {
            _localStorageService = localStorageService;
            _httpService = httpService;
            _mapper = mapper;
        }


        public async Task AddTodoItemAsync(Guid todoListId, TodoItem todoItem)
        {
            var dto = new NewTodoItemDto
            {
                ListId = todoListId.ToString(),
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

        public async Task UpdateItem(Guid id, TodoItem todoItem)
        {
            var dto = _mapper.Map<UpdateTodoItemDto>(todoItem);
            dto.ListId = id.ToString();

            await _httpService.PutVoidAsync(FunctionConstants.UpdateTodoItemFunction, dto);
        }

        public async Task DeleteItem(Guid listId, string id)
        {
            await _httpService.DeleteAsync($"{FunctionConstants.DeleteTodoItemFunction}/{listId}/{id}");


        }

        public async Task<IEnumerable<TodoItem>> GetList(Guid id)
        {
            var dtos = await _httpService.GetAsync<List<TodoItemDto>>(FunctionConstants.GetTodoListFunction + $"?id={id}");
            Console.WriteLine(JsonSerializer.Serialize(dtos));
            var models = _mapper.Map<IEnumerable<TodoItem>>(dtos);
            return models;
        }


        public async Task<Guid> GetListId()
        {
            if (await _localStorageService.ContainKeyAsync(StorageIdKey))
            {
                return await _localStorageService.GetItemAsync<Guid>(StorageIdKey);

            }

            var id = Guid.NewGuid();
            await _localStorageService.SetItemAsync(StorageIdKey, id);
            return id;
        }


    }
}
