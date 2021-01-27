using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Todo.Blazor.Models;
using Todo.Blazor.Services.Interfaces;
using Todo.Shared.Dto.TodoLists.Members;

namespace Todo.Blazor.Pages.TodoList
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


        public async Task AddNewMember(TodoListMember member)
        {
            TodoList.Members.Add(member); 
            await TodoListMemberService.AddMemberToTodoList(new NewTodoListMemberDto
                {ListId = TodoList.Id, UserId = member.Id});
        }

        public async Task MemberDelete(TodoListMember member)
        {
            TodoList.Members.Remove(member);
            await TodoListMemberService.RemoveMemberFromTodoList(TodoList.Id, member.Id);
        }

    }
}
