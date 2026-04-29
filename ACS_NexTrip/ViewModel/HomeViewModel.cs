using System.Collections.ObjectModel;
using ACS_NexTrip.Models;
using ACS_NexTrip.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace ACS_NexTrip.ViewModel
{
    public partial class HomeViewModel : ObservableObject
    {
        private readonly ConnexionBD _db;

        [ObservableProperty]
        private ObservableCollection<Trajet> _trajets;

        public HomeViewModel(ConnexionBD db)
        {
            _db = db;
            ChargerTrajets();
        }

        public async void ChargerTrajets()
        {
            Trajets = new ObservableCollection<Trajet>(await _db.GetNextTrajetsAsync());
        }

        // [RelayCommand] génère "NavigateToDashboardCommand" automatiquement
        [RelayCommand]
        private async Task NavigateToDashboard() =>
            await Shell.Current.GoToAsync("HomePage");

        [RelayCommand]
        private async Task NavigateToTrips() =>
            await Shell.Current.GoToAsync("TrajetPage");

        [RelayCommand]
        private async Task NavigateToUsers() =>
            await Shell.Current.GoToAsync("SettingsPage");

        [RelayCommand]
        private void ShowNotifications() { /* À implémenter */ }

        [RelayCommand]
        private void ShowProfile() { /* À implémenter */ }
    }
}