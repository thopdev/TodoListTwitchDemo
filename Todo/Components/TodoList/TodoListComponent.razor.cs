using System;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Todo.Blazor.Models;
using Todo.Blazor.Services.Interfaces;

namespace Todo.Blazor.Components.TodoList
{
    public partial class TodoListComponent
    {
        public bool Editing { get; set; }

        [Parameter] public Models.TodoList TodoList { get; set; }

        [Parameter] public EventCallback<Models.TodoList> OnChange { get; set; }
        [Parameter] public EventCallback<Models.TodoList> OnDelete { get; set; }


        [Inject]
        public ITodoListMemberService TodoListMemberService { get; set; }

        public async Task OnSave()
        {
            Editing = false;
            await OnChange.InvokeAsync(TodoList);
        }

        public void Edit()
        {
            Editing = true;
        }

        public async Task Delete()
        {
            await OnDelete.InvokeAsync(TodoList);
        }


        public async Task AddNewMember(User member)
        {
            TodoList.Members.Add(new TodoListShare{Role = 0, Member = member}); 
            await TodoListMemberService.AddMemberToTodoList(TodoList.Id, member);
        }

        public async Task MemberDelete(TodoListShare share)
        {
            TodoList.Members.Remove(share);
            await TodoListMemberService.RemoveMemberFromTodoList(TodoList.Id, share.Member.UserId);
        }

        public async Task ShareChange(TodoListShare share)
        {
            await TodoListMemberService.UpdateMemberShare(TodoList.Id, share);
        }
    }
}
