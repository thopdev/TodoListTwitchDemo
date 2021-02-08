using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Components;
using Todo.Blazor.Components.Users;
using Todo.Blazor.Models;
using Todo.Shared.Enums;

namespace Todo.Blazor.Components.TodoList
{
    public partial class TodoListShareOptionComponent
    {
        [Parameter]
        public List<TodoListShare> Members { get; set; }

        [Parameter]
        public EventCallback<User> OnNewMember { get; set; }

        [Parameter]
        public EventCallback<TodoListShare> OnMemberDelete { get; set; }

        [Parameter]
        public EventCallback<TodoListShare> OnShareChange { get; set; }

        public UserTypeAheadComponent UserTypeAhead { get; set; }

        public void ChangeShare(ChangeEventArgs args, TodoListShare share)
        {
            if (args.Value is string stringValue)
            {
                share.Role = Enum.Parse<ShareRole>(stringValue);
                OnShareChange.InvokeAsync(share);
            }
        }
        
        public void Add()
        {
            OnNewMember.InvokeAsync(UserTypeAhead.NewUser);
        }

        public void Delete(TodoListShare member)
        {
            OnMemberDelete.InvokeAsync(member);
        }
    }
}
