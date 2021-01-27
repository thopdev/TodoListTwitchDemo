using System.Collections.Generic;

namespace Todo.Blazor.Models
{
    public class TodoList
    {
        private List<TodoListMember> _members;
        public string Id { get; set; }
        public string Name { get; set; }

        public List<TodoListMember> Members
        {
            get => _members ??= new List<TodoListMember>();
            set => _members = value;
        }
    }

    public class TodoListMember
    {
        public string Id { get; set; }
    }
}
