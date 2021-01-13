using Todo.Shared.Enums;

namespace Todo.Shared.Dto.TodoItems
{
    public class NewTodoItemDto
    { 
        public string Name { get; set; }
        public bool Status { get; set; }
        public TodoItemPriority Priority { get; set; }
    }
}
