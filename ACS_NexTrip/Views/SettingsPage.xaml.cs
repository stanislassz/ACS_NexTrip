using ACS_NexTrip.Services;

namespace ACS_NexTrip.Views
{
    public partial class SettingsPage : ContentPage
    {
        public SettingsPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            var service = new ConnexionBD();
            var result = await service.GetUtilisateurAsync();

            // On force MAUI à mettre à jour l'interface sur le thread principal
            MainThread.BeginInvokeOnMainThread(() =>
            {
                UtilisateurCollection.ItemsSource = result;
            });
        }
    }
}
