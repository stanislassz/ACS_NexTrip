using System.Collections.ObjectModel;
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
        private ObservableCollection<Utilisateur> _utilisateurs = new();

        public UserViewModel(ConnexionBD db)
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
        private async Task NavigateToSettings() =>
            await Shell.Current.GoToAsync("SettingsPage");

        [RelayCommand]
        private void ShowNotifications() { /* À implémenter */ }

        [RelayCommand]
        private void ShowProfile() { /* À implémenter */ }

        // --- Actions ---

        //[RelayCommand]
        //private async Task GoToAddUtilisateur() =>
        //    await Shell.Current.GoToAsync(nameof(Pages.AddUtilisateurPage));

        // Appelé automatiquement à chaque fois que la page devient visible
        [RelayCommand]
        private async Task Appearing() => await GetUtilisateurs();

        [RelayCommand]
        private async Task GetUtilisateurs()
        {
            var data = await _db.GetUtilisateursAsync();
            Utilisateurs.Clear();
            foreach (var t in data)
                Utilisateurs.Add(t);
        }

        [RelayCommand]
        private async Task GoToEdit(Utilisateur UtilisateurSelectionne)
        {
            if (UtilisateurSelectionne == null) return;

            var parametres = new Dictionary<string, object>
        {
            { "UtilisateurEchange", UtilisateurSelectionne }
        };
            // On navigue vers ta nouvelle page de modification
            await Shell.Current.GoToAsync("EditUtilisateurPage", parametres);
        }

        [RelayCommand]
        private async Task DeleteUtilisateur(Utilisateur utilisateur)
        {
            if (utilisateur == null) return;

            bool success = await _db.DeleteTrajetAsync(utilisateur.UTI_ID);

            if (success)
                Utilisateurs.Remove(utilisateur);
            else
                await App.Current.MainPage.DisplayAlert(
                    "Erreur",
                    "Impossible de supprimer ce trajet. Vérifiez s'il n'est pas lié à d'autres données.",
                    "OK");
        }
    }
}