using System.Collections.ObjectModel;
using ACS_NexTrip.Models;
using ACS_NexTrip.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace ACS_NexTrip.ViewModel
{
    public partial class SettingsViewModel : ObservableObject
    {
        private readonly ConnexionBD _db;

        [ObservableProperty]
        private ObservableCollection<Utilisateur> _utilisateurs;

        public SettingsViewModel(ConnexionBD db)
        {
            _db = db;
            ChargerUtilisateur();
        }

        public async void ChargerUtilisateur()
        {
            Utilisateurs = new ObservableCollection<Utilisateur>(await _db.GetUtilisateurAsync());
        }

        // Commandes de navigation
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