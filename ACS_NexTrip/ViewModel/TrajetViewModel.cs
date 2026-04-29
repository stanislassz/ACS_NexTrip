using System.Collections.ObjectModel;
using ACS_NexTrip.Models;
using ACS_NexTrip.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace ACS_NexTrip.ViewModel
{
    public partial class TrajetViewModel : ObservableObject
    {
        private readonly ConnexionBD _db;

        [ObservableProperty]
        private ObservableCollection<Trajet> _trajets = new();

        public TrajetViewModel(ConnexionBD db)
        {
            _db = db;
            // Plus de chargement ici — c'est Appearing qui s'en charge
            // pour éviter deux requêtes simultanées sur la même connexion
        }

        // --- Navigation ---

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

        // --- Actions ---

        [RelayCommand]
        private async Task GoToAddTrajet() =>
            await Shell.Current.GoToAsync(nameof(Pages.AddTrajetPage));

        // Appelé automatiquement à chaque fois que la page devient visible
        [RelayCommand]
        private async Task Appearing() => await GetTrajets();

        [RelayCommand]
        private async Task GetTrajets()
        {
            var data = await _db.GetTrajetsAsync();
            Trajets.Clear();
            foreach (var t in data)
                Trajets.Add(t);
        }

        [RelayCommand]
        private async Task DeleteTrajet(Trajet trajet)
        {
            if (trajet == null) return;

            bool success = await _db.DeleteTrajetAsync(trajet.TRA_ID);

            if (success)
                Trajets.Remove(trajet);
            else
                await App.Current.MainPage.DisplayAlert(
                    "Erreur",
                    "Impossible de supprimer ce trajet. Vérifiez s'il n'est pas lié à d'autres données.",
                    "OK");
        }
    }
}