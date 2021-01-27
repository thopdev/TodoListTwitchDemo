using Todo.Blazor.Models;

namespace Todo.Blazor.Factories.Interfaces
{
    public interface ILoaderItemFactory
    {
        LoaderItem CreateRandomItem();
    }
}