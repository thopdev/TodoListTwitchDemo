using System;
using System.Collections.Generic;
using System.Text;

namespace Todo.Shared.Dto.TodoLists
{
    public class UpdateTodoListDto
    {
        public string ListId { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
    }
}
