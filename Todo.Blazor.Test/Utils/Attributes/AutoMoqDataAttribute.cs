using System;
using System.Reflection;
using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.Kernel;
using AutoFixture.Xunit2;
using Bunit;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Todo.Blazor.Factories.Interfaces;
using Todo.Blazor.Services.Interfaces;
using Fixture = AutoFixture.Fixture;

namespace Todo.Blazor.Test.Utils.Attributes
{
    public class AutoMoqDataAttribute : AutoDataAttribute
    {
        public AutoMoqDataAttribute() : base(() => 
            new Fixture()
                .Customize(new AutoMoqCustomization())
                .Customize(new AddAllServicesCustomization()))
        {

        }
    }

    public class AddAllServicesCustomization : ICustomization
    {
        public void Customize(IFixture fixture)
        {
            fixture.Customize(new AddServiceCustomization<ILoaderItemFactory>());
            fixture.Customize(new AddServiceCustomization<ITodoListMemberService>());

        }
    }

    public class AddServiceCustomization<T> : ICustomization where T : class
    {
        public void Customize(IFixture fixture)
        {
            fixture.Customize<Mock>(c => 
                c.Do(m => 
                    fixture.Freeze<TestContext>().Services.AddSingleton(m.Object)
            ));
        }
    }

}
