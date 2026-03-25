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
            NavigateToDashboardCommand = new Command(async () => await GoToHomeAsync());
            NavigateToTripsCommand = new Command(async () => await GoToTripsAsync());
            NavigateToUsersCommand = new Command(async () => await GoToSettingsAsync());
            ShowNotificationsCommand = new Command(() => { /* À implémenter */ });
            ShowProfileCommand = new Command(() => { /* À implémenter */ });
        }

        private async Task GoToTripsAsync()
        {
            await Shell.Current.GoToAsync("TrajetPage");
        }

        private async Task GoToSettingsAsync()
        {
            await Shell.Current.GoToAsync("SettingsPage");
        }
        private async Task GoToHomeAsync()
        {
            await Shell.Current.GoToAsync("HomePage");
        }
    }
}
