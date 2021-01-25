using Todo.Shared.Enums;

namespace Todo.Shared.Dto.TodoItems
{
    public class UpdateTodoItemDto
    {

        public string ListId { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
        public bool Status { get; set; }
        public TodoItemPriority Priority { get; set; }


    }
}
