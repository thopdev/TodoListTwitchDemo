using System.Text.Json.Serialization;
using Todo.Shared.Enums;

namespace Todo.Shared.Dto.TodoItems
{
    public class TodoItemDto
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("status")]
        public bool Status { get; set; }

        [JsonPropertyName("priority")]

        public TodoItemPriority Priority { get; set; }


    }
}
