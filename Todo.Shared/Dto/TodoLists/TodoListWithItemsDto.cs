using System.Collections.Generic;
using Todo.Shared.Dto.TodoItems;
using Todo.Shared.Enums;

namespace Todo.Shared.Dto.TodoLists
{
    public class TodoListWithItemsDto
    {
        public string Id { get; set; }
        public ShareRole ShareRole { get; set; }
        public List<TodoItemDto> Items { get; set; }
    }
}
