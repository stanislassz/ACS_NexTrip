namespace ACS_NexTrip.ViewModel
{
    using ACS_NexTrip.Pages;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using Microsoft.Maui.Controls;

    public class HomeViewModel
    {
        // Commandes pour les bindings du XAML
        public ICommand NavigateToDashboardCommand { get; }
        public ICommand NavigateToTripsCommand { get; }
        public ICommand NavigateToUsersCommand { get; }
        public ICommand ShowNotificationsCommand { get; }
        public ICommand ShowProfileCommand { get; }

        public HomeViewModel()
        {
            // Initialisation des commandes
            NavigateToDashboardCommand = new Command(() => { /* Déjà sur l'accueil */ });
            NavigateToTripsCommand = new Command(async () => await GoToTripsAsync());
            NavigateToUsersCommand = new Command(async () => await GoToUsersAsync());
            ShowNotificationsCommand = new Command(() => { /* À implémenter */ });
            ShowProfileCommand = new Command(() => { /* À implémenter */ });
        }

        private async Task GoToTripsAsync()
        {
            //Dans un ViewModel, on utilise Shell ou Application.Current.MainPage pour naviguer
            if (Application.Current?.MainPage != null)
            {
                await Application.Current.MainPage.Navigation.PushAsync(new TrajetPage());
            }
        }

        private async Task GoToUsersAsync()
        {
            if (Application.Current?.MainPage != null)
            {
                await Application.Current.MainPage.Navigation.PushAsync(new SettingsPage());
            }
        }
    }
}
