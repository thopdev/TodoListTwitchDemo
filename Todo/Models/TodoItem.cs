using System;
using Todo.Shared.Enums;

namespace Todo.Blazor.Models
{
    public class TodoItem
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public bool Status { get; set; }
        public TodoItemPriority Priority { get; set; }
        public TodoItemScheduleType ScheduledType { get; set; }
        public bool[] RepeatOnDay { get; set; } = new bool[Enum.GetValues(typeof(DayOfWeek)).Length];
    }
}
