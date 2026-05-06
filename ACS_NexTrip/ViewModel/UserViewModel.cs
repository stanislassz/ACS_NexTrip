using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ACS_NexTrip.Models;
using ACS_NexTrip.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace ACS_NexTrip.ViewModel
{
    public partial class UserViewModel : ObservableObject
    {
        private readonly ConnexionBD _db;

        [ObservableProperty]
        private ObservableCollection<Utilisateur> _utilisateurs;

        public UserViewModel(ConnexionBD db)
        {
            _db = db;
        }


        [RelayCommand]
        private async Task NavigateToDashboard() =>
            await Shell.Current.GoToAsync("HomePage");

        [RelayCommand]
        private async Task NavigateToTrips() =>
            await Shell.Current.GoToAsync("TrajetPage");

        [RelayCommand]
        private async Task NavigateToSettings() =>
            await Shell.Current.GoToAsync("SettingsPage");

        [RelayCommand]
        private async Task NavigateToUsers() =>
            await Shell.Current.GoToAsync("UsersPage");

        [RelayCommand]
        private async Task NavigateToLieu() =>
            await Shell.Current.GoToAsync("LieuPage");

        [RelayCommand]
        private void ShowNotifications() { /* À implémenter */ }

        [RelayCommand]
        private void ShowProfile() { /* À implémenter */ }
    }
}
