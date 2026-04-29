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