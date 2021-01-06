using Todo.Shared.Enums;

namespace Todo.Shared.Dto
{
    public class UpdateTodoItemDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public bool Status { get; set; }
        public TodoItemPriority Priority { get; set; }


    }
}
