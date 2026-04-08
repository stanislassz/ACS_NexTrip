using ACS_NexTrip.Pages;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.Maui.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using ACS_NexTrip.Models;
using ACS_NexTrip.Services;
using CommunityToolkit.Mvvm.Input;

namespace ACS_NexTrip.ViewModel
{
    public partial class HomeViewModel : ObservableObject
    {
        // Commandes pour les bindings du XAML
        public ICommand NavigateToDashboardCommand { get; }
        public ICommand NavigateToTripsCommand { get; }
        public ICommand NavigateToUsersCommand { get; }
        public ICommand ShowNotificationsCommand { get; }
        public ICommand ShowProfileCommand { get; }
        public ICommand DeleteTrajetCommand { get; }

        private readonly ConnexionBD _db;

        [ObservableProperty]
        private ObservableCollection<Trajet> _trajets;

        public HomeViewModel()
        {
            // Initialisation des commandes
            NavigateToDashboardCommand = new Command(async () => await GoToHomeAsync());
            NavigateToTripsCommand = new Command(async () => await GoToTripsAsync());
            NavigateToUsersCommand = new Command(async () => await GoToSettingsAsync());
            ShowNotificationsCommand = new Command(() => { /* À implémenter */ });
            ShowProfileCommand = new Command(() => { /* À implémenter */ });
            DeleteTrajetCommand = new Command<Trajet>((trajet) => {
                if (trajet != null && Trajets != null && Trajets.Contains(trajet))
                {
                    Trajets.Remove(trajet);
                }
            });

            _db = new ConnexionBD();
            ChargerTrajets();
        }

        public HomeViewModel(ConnexionBD db)
        {
            // Initialisation des commandes
            NavigateToDashboardCommand = new Command(async () => await GoToHomeAsync());
            NavigateToTripsCommand = new Command(async () => await GoToTripsAsync());
            NavigateToUsersCommand = new Command(async () => await GoToSettingsAsync());
            ShowNotificationsCommand = new Command(() => { /* À implémenter */ });
            ShowProfileCommand = new Command(() => { /* À implémenter */ });
            DeleteTrajetCommand = new Command<Trajet>((trajet) => {
                if (trajet != null && Trajets != null && Trajets.Contains(trajet))
                {
                    Trajets.Remove(trajet);
                }
            });

            _db = db;
            ChargerTrajets();
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

        public async void ChargerTrajets()
        {
            var prochainsTrajets = await _db.GetNextTrajetsAsync();
            MainThread.BeginInvokeOnMainThread(() =>
            {
                Trajets = new ObservableCollection<Trajet>(prochainsTrajets);
            });
        }
    }
}
