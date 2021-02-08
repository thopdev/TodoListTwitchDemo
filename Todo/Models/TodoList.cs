using System.Collections.Generic;

namespace Todo.Blazor.Models
{
    public class TodoList
    {
        private List<TodoListShare> _members;
        public string Id { get; set; }
        public string Name { get; set; }

        public List<TodoListShare> Members
        {
            get => _members ??= new List<TodoListShare>();
            set => _members = value;
        }
    }
}
