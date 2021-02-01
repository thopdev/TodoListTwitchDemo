using System.Collections.Generic;
using Microsoft.AspNetCore.Components;
using Todo.Blazor.Components.Users;
using Todo.Blazor.Models;

namespace Todo.Blazor.Components.TodoList
{
    public partial class TodoListShareOptionComponent
    {
        [Parameter]
        public List<TodoListMember> Members { get; set; }

        [Parameter]
        public EventCallback<TodoListMember> OnNewMember { get; set; }


        [Parameter]
        public EventCallback<TodoListMember> OnMemberDelete { get; set; }

        public UserTypeAheadComponent UserTypeAhead { get; set; }

        private string NewMemberId { get; set; }


        public void Add()
        {
            OnNewMember.InvokeAsync(new TodoListMember {Id = UserTypeAhead.NewUser.UserId});
            NewMemberId = string.Empty;
        }

        public void Delete(TodoListMember member)
        {
            OnMemberDelete.InvokeAsync(member);
        }

    }
}
