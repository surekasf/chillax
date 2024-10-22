namespace Chillax
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();


            // Register routes if you have additional pages that require navigation
            Routing.RegisterRoute(nameof(MainPage), typeof(MainPage));
            Routing.RegisterRoute(nameof(UserProfilePage), typeof(UserProfilePage));

        }
    }
}
