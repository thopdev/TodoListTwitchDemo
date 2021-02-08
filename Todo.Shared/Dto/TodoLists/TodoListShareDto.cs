using Todo.Shared.Enums;

namespace Todo.Shared.Dto.TodoLists
{
    public class TodoListShareDto
    {
        public UserDto Member { get; set; }
        public ShareRole Role { get; set; }
    }
}