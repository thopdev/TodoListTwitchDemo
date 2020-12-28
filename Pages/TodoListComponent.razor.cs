using System.Collections.Generic;
using Todo.Models;

namespace Todo.Pages
{
    public partial class TodoListComponent
    {
        public IList<TodoItem> TodoItems { get; } = new List<TodoItem>{new TodoItem{Name= "Add Css"}, new TodoItem{Name = "Add API"}, new TodoItem { Name = "Grab a drink" } };

        public string NewTodoItemName { get; set; }

        public void AddTodoItemToList()
        {
            TodoItems.Add(new TodoItem(){Name = NewTodoItemName});
            NewTodoItemName = string.Empty;
        }
    }
}
