using Todo.Shared.Enums;

namespace Todo.Shared.Dto.TodoItems
{
    public class TodoItemDto
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public bool Status { get; set; }

        public TodoItemPriority Priority { get; set; }

        public TodoItemScheduleType ScheduledType { get; set; }
        
        public bool[] RepeatOnDay { get; set; }
    }
}
