using System;
using Microsoft.Azure.Cosmos.Table;
using Todo.Shared.Enums;

namespace Todo.AzureFunctions.Entities
{
    public class TodoItemEntity : TableEntity
    {
        public string Name { get; set; }
        public bool Status { get; set; }
        public int Priority { get; set; }
        public int ScheduledType { get; set; }

        [IgnoreProperty] 
        public bool[] RepeatOnDay { get; set; } = new bool[Enum.GetValues(typeof(DayOfWeek)).Length];

        public bool Monday
        {
            get => RepeatOnDay[(int)DayOfWeek.Monday];
            set => RepeatOnDay[(int)DayOfWeek.Monday] = value;
        }

        public bool Tuesday
        {
            get => RepeatOnDay[(int)DayOfWeek.Tuesday];
            set => RepeatOnDay[(int)DayOfWeek.Tuesday] = value;
        }
        public bool Wednesday
        {
            get => RepeatOnDay[(int)DayOfWeek.Wednesday];
            set => RepeatOnDay[(int)DayOfWeek.Wednesday] = value;
        }
        public bool Thursday
        {
            get => RepeatOnDay[(int)DayOfWeek.Thursday];
            set => RepeatOnDay[(int)DayOfWeek.Thursday] = value;
        }
        public bool Friday
        {
            get => RepeatOnDay[(int)DayOfWeek.Friday];
            set => RepeatOnDay[(int)DayOfWeek.Friday] = value;
        }
        public bool Saturday
        {
            get => RepeatOnDay[(int)DayOfWeek.Saturday];
            set => RepeatOnDay[(int)DayOfWeek.Saturday] = value;
        }
        public bool Sunday
        {
            get => RepeatOnDay[(int)DayOfWeek.Sunday];
            set => RepeatOnDay[(int)DayOfWeek.Sunday] = value;
        }
    }
}
