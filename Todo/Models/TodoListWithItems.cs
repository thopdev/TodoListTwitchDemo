using System.Collections.Generic;
using Todo.Shared.Enums;

namespace Todo.Blazor.Models
{
    public class TodoListWithItems
    {
        public string Id { get; set; }
        public ShareRole ShareRole { get; set; }
        public List<TodoItem> Items { get; set; }
    }
}
