using AutoFixture.Xunit2;

namespace Todo.Blazor.Test.Utils.Attributes
{
    public class InlineDomainDataAttribute : InlineAutoDataAttribute
    {
        public InlineDomainDataAttribute(params object[] values) : base(new AutoMoqDataAttribute(), values)
        {
        }
    }
}