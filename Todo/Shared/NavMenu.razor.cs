using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace Todo.Shared
{
    public partial class NavMenu
    {
        [Inject] private AuthenticationStateProvider AuthenticationStateProvider { get; set; }





    }
}
