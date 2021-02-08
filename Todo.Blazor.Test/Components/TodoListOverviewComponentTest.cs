using System.Collections.Generic;
using Bunit;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Todo.Blazor.Components.TodoList;
using Todo.Blazor.Factories.Interfaces;
using Todo.Blazor.Models;
using Todo.Blazor.Services.Interfaces;
using Todo.Blazor.Test.Utils.Attributes;
using Xunit;

namespace Todo.Blazor.Test.Components
{
    public class TodoListOverviewComponentTest
    {
        [Theory, InlineDomainData]
        public void NoList(Mock<ITodoListService> todoListMock,  Mock<ILoaderItemFactory> loaderFactoryMock, TestContext context)
        {
            var todoListOverviewComponent = context.RenderComponent<TodoListsOverviewComponent>();


            Assert.Single(todoListOverviewComponent.FindAll("lottie-player"));
        }

        [Theory, InlineDomainData]
        public void LoadsComponentsCorrectly(List<TodoList> todoLists, Mock<ITodoListService> todoListMock, Mock<ITodoListMemberService> todoListMemberServiceMock, TestContext context)
        {
            todoListMock.Setup(t => t.GetAllLists()).ReturnsAsync(todoLists);


            var todoListOverviewComponent = context.RenderComponent<TodoListsOverviewComponent>();

            var compontents = todoListOverviewComponent.FindComponents<TodoListComponent>();
            Assert.Equal(3, compontents.Count);

            Assert.NotNull(todoListOverviewComponent.FindComponent<NewTodoListComponent>());
        }
    }
}
