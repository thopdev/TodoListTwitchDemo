using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Components;
using Todo.Models;

namespace Todo.Pages.TodoList
{
    public partial class TodoListShareOptionComponent
    {
        [Parameter]
        public List<TodoListMember> Members { get; set; }

        [Parameter]
        public EventCallback<TodoListMember> OnNewMember { get; set; }


        [Parameter]
        public EventCallback<TodoListMember> OnMemberDelete { get; set; }


        private string NewMemberId { get; set; }


        public void Add()
        {
            OnNewMember.InvokeAsync(new TodoListMember {Id = NewMemberId});
            NewMemberId = string.Empty;
        }

        public void Delete(TodoListMember member)
        {
            OnMemberDelete.InvokeAsync(member);
        }

    }
}
