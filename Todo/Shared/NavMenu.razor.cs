using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace Todo.Shared
{
    public partial class NavMenu
    {
        [Inject] private AuthenticationStateProvider AuthenticationStateProvider { get; set; }

        protected override void OnInitialized()
        {
            UpdateUserName();
            AuthenticationStateProvider.AuthenticationStateChanged += (task) => UpdateUserName();
        }

        private string UserName { get; set; }

        private async void UpdateUserName()
        {
            var user = await AuthenticationStateProvider.GetAuthenticationStateAsync();
            UserName = user.User.Identity?.Name;


        }
    }
}
