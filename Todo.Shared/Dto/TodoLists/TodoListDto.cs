using System.Collections.Generic;

namespace Todo.Shared.Dto.TodoLists
{
    public class TodoListDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public List<TodoListShareDto> Members { get; set; }
    }
}
