using Microsoft.AspNetCore.Components;

namespace Todo.Blazor.Pages.TodoListItems
{
    public partial class TodoListItemsPage
    {

        [Parameter] public string ListId { get; set; }

    }
}
