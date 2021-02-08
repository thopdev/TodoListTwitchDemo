using Todo.Shared.Enums;

namespace Todo.Blazor.Models
{
    public class TodoListShare
    {
        public User Member { get; set; }
        public ShareRole Role { get; set; }
    }
}
