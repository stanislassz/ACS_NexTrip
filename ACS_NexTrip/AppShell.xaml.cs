using Microsoft.Maui.Controls;
using ACS_NexTrip.Pages;

namespace ACS_NexTrip
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            // On enregistre la route pour pouvoir y naviguer plus tard
            // "nameof(HomePage)" donne le texte "HomePage"
            Routing.RegisterRoute("LoginPage", typeof(ACS_NexTrip.Pages.LoginPage));
            Routing.RegisterRoute("RegisterPage", typeof(ACS_NexTrip.Pages.RegisterPage));
            Routing.RegisterRoute("HomePage", typeof(ACS_NexTrip.Pages.HomePage));
            Routing.RegisterRoute("TrajetPage", typeof(ACS_NexTrip.Pages.TrajetPage));
            Routing.RegisterRoute("SettingsPage", typeof(ACS_NexTrip.Pages.SettingsPage));
            Routing.RegisterRoute("AddTrajetPage", typeof(ACS_NexTrip.Pages.AddTrajetPage));
        }
    }
}
