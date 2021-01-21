using Microsoft.AspNetCore.Components;
using Todo.Factories.Interfaces;
using Todo.Models;

namespace Todo.Pages
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
