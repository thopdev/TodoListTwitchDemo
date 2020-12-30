using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Todo.Models;
using Todo.Services.Interfaces;

namespace Todo.Pages
{
    public partial class TodoListComponent
    {
        [Inject]
        public ITodoListService TodoListService { get; set; }

        public ObservableCollection<TodoItem> TodoItems { get; set; }


        protected override async Task OnInitializedAsync()
        {
            await Task.Delay(1000);
            TodoItems =  new ObservableCollection<TodoItem>(await TodoListService.Get());
            TodoItems.CollectionChanged += (object sender, NotifyCollectionChangedEventArgs args) => Save();
        }

        public void AddTodoItemToList(TodoItem todoItem)
        {
            TodoItems.Add(todoItem);
            Save();
        }

        public void RemoveFromTodoList(TodoItem item)
        {
            TodoItems.Remove(item);
            Save();

        }

        public void Save()
        {
            TodoListService.Save(TodoItems);
        }
    }
}
