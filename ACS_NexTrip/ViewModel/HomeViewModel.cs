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
        private ObservableCollection<Trajet> _trajets = new ObservableCollection<Trajet>();

        public HomeViewModel(ConnexionBD db)
        {
            _db = db;
        }

        [RelayCommand]
        private async Task Appearing() => await GetNextTrajets();

        [RelayCommand]
        private async Task GetNextTrajets()
        {
            var data = await _db.GetNextTrajetsAsync();
            Trajets.Clear();
            foreach (var t in data)
                Trajets.Add(t);
        }



        [RelayCommand]
        private async Task NavigateToDashboard() =>
            await Shell.Current.GoToAsync("HomePage");

        [RelayCommand]
        private async Task NavigateToTrips() =>
            await Shell.Current.GoToAsync("TrajetPage");

        [RelayCommand]
        private async Task NavigateToLieu() =>
            await Shell.Current.GoToAsync("LieuPage");

        [RelayCommand]
        private async Task NavigateToUsers() =>
            await Shell.Current.GoToAsync("UsersPage");

        [RelayCommand]
        private async Task NavigateToSettings() =>
            await Shell.Current.GoToAsync("SettingsPage");
    }
}