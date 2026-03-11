using Microsoft.Maui.Controls;
using ACS_NexTrip.Views;

namespace ACS_NexTrip
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            // On enregistre la route pour pouvoir y naviguer plus tard
            // "nameof(HomePage)" donne le texte "HomePage"
            Routing.RegisterRoute("RegisterPage", typeof(Views.RegisterPage));
            Routing.RegisterRoute("HomePage", typeof(Views.HomePage));
        }
    }
}
