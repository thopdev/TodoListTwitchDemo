namespace Todo.Blazor.Shared
{
    public partial class MainLayout
    {
        public LoginProvider[] LoginProviders { get; } = { new LoginProvider("Twitter", "TwitterLogo.svg"), new LoginProvider("Google", "GoogleLogo.svg"), new LoginProvider("Microsoftaccount", "MicrosoftLogo.svg"), new LoginProvider("Facebook", "FacebookLogo.svg") };
    }


    public class LoginProvider
    {

        public LoginProvider(string name, string logoName)
        {
            Name = name;
            LogoName = logoName;
        }

        public string Name { get; }
        public string LogoName { get; }
    }
}
