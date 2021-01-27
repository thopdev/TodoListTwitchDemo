using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bunit;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Todo.Blazor.Components.TodoList;
using Todo.Blazor.Factories.Interfaces;
using Todo.Blazor.Models;
using Todo.Blazor.Services;
using Todo.Blazor.Services.Interfaces;
using Todo.Blazor.Test.Utils.Attributes;
using Xunit;

namespace Todo.Blazor.Test.Components
{
    public class TodoListOverviewComponentTest : TestContext
    {
        [Theory, InlineDomainData]
        public void NoList(Mock<ITodoListService> todoListMock, Mock<ILoaderItemFactory> loaderFactoryMock)
        {
            Services.AddSingleton(loaderFactoryMock.Object);
            Services.AddSingleton(todoListMock.Object);

            var todoListOverviewComponent = RenderComponent<TodoListsOverviewComponent>();


            Assert.Single(todoListOverviewComponent.FindAll("lottie-player"));
        }

        [Theory, InlineDomainData]
        public void LoadsComponentsCorrectly(List<TodoList> todoLists, Mock<ITodoListService> todoListMock, Mock<ITodoListMemberService> todoListMemberServiceMock)
        {
            Services.AddSingleton(todoListMock.Object);
            Services.AddSingleton(todoListMemberServiceMock.Object);

            todoListMock.Setup(t => t.GetAllLists()).ReturnsAsync(todoLists);


            var todoListOverviewComponent = RenderComponent<TodoListsOverviewComponent>();

            var compontents = todoListOverviewComponent.FindComponents<TodoListComponent>();
            Assert.Equal(3, compontents.Count);

            Assert.NotNull(todoListOverviewComponent.FindComponent<NewTodoListComponent>());
        }
    }
}
