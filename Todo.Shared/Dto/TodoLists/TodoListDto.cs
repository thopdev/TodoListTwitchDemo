using System.Collections.Generic;
using Todo.Shared.Dto.TodoLists.Members;

namespace Todo.Shared.Dto.TodoLists
{
    public class TodoListDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public List<TodoListMemberDto> Members { get; set; }
    }
}
