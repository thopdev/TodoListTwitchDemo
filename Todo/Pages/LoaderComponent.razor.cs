using Microsoft.AspNetCore.Components;
using Todo.Blazor.Factories.Interfaces;
using Todo.Blazor.Models;

namespace Todo.Blazor.Pages
{
    public partial class LoaderComponent
    {
        [Inject] public ILoaderItemFactory LoaderItemFactory { get; set; }

        public LoaderItem LoaderItem { get; set; }

        protected override void OnInitialized()
        {
            LoaderItem = LoaderItemFactory.CreateRandomItem();
        }
    }
}
