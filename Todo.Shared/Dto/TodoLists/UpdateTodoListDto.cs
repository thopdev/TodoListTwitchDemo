namespace Todo.Shared.Dto.TodoLists
{
    public class UpdateTodoListDto
    {
        public string OwnerId { get; set; }
        public string ListId { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
    }
}
