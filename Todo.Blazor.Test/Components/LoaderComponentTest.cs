using System.Linq;
using Bunit;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Todo.Blazor.Components;
using Todo.Blazor.Factories.Interfaces;
using Todo.Blazor.Models;
using Todo.Blazor.Test.Utils.Attributes;
using Xunit;

namespace Todo.Blazor.Test.Components
{
    public class LoaderComponentTest : TestContext
    {
        [Theory]
        [InlineDomainData]
        public void LoaderContainsLottiePlayer(string name, string uri, Mock<ILoaderItemFactory> loaderItemFactory)
        {
            Services.AddSingleton(loaderItemFactory.Object);

            loaderItemFactory.Setup(x => x.CreateRandomItem()).Returns(new LoaderItem(name, uri));

            var loaderComponent = RenderComponent<LoaderComponent>();

            var lottiePlayer = loaderComponent.Find("lottie-player");
            Assert.Equal(uri, lottiePlayer.Attributes.FirstOrDefault(a => a.Name == "src").Value);
        }

        [Theory]
        [InlineDomainData]
        public void LoaderWithoutUsername(string name, string uri, Mock<ILoaderItemFactory> loaderItemFactory)
        {
            Services.AddSingleton(loaderItemFactory.Object);

            loaderItemFactory.Setup(x => x.CreateRandomItem()).Returns(new LoaderItem(name, uri));

            var loaderComponent = RenderComponent<LoaderComponent>();

            var spans = loaderComponent.FindAll("span");

            var nameSpan = Assert.Single(spans);
            Assert.Equal(name, nameSpan.TextContent);
        }

        [Theory]
        [InlineDomainData]
        public void LoaderWithUsername(string name, string uri, string userName, Mock<ILoaderItemFactory> loaderItemFactory)
        {
            Services.AddSingleton(loaderItemFactory.Object);

            loaderItemFactory.Setup(x => x.CreateRandomItem()).Returns(new LoaderItem(name, uri, userName));

            var loaderComponent = RenderComponent<LoaderComponent>();

            var spans = loaderComponent.FindAll("span");

            Assert.Equal(2, spans.Count);
            Assert.Equal(name, spans[0].TextContent);
            Assert.Equal(userName, spans[1].TextContent);
        }

    }
}
