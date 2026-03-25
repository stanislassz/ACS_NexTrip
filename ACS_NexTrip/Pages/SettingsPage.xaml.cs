using ACS_NexTrip.Services;

namespace ACS_NexTrip.Pages
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

        private async void OnTrajetsClicked(object sender, EventArgs e)
        {
            // On navigue vers la page des trajets
            // Assure-toi que le nom de ta classe est bien TrajetPage
            await Navigation.PushAsync(new TrajetPage());
        }

        private async void OnUtilisateurClicked(object sender, EventArgs e)
        {
            // On navigue vers la page des trajets
            // Assure-toi que le nom de ta classe est bien TrajetPage
            await Navigation.PushAsync(new SettingsPage());
        }
    }
}
