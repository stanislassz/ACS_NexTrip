using ACS_NexTrip.Models;
using ACS_NexTrip.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace ACS_NexTrip.ViewModel
{
    // 1. AJOUT DU MOT-CLÉ partial
    public partial class AddLieuViewModel : ObservableObject
    {
        private readonly ConnexionBD _db;

        [ObservableProperty]
        private string _libelle;

        // 2. AJOUT DU CONSTRUCTEUR (pour l'injection de dépendances)
        public AddLieuViewModel(ConnexionBD db)
        {
            _db = db;
        }

        [RelayCommand]
        private async Task Save()
        {
            // Vérification simple
            if (string.IsNullOrWhiteSpace(Libelle)) return;

            var nouveau = new Lieu
            {
                LIE_LIBELLE = Libelle
            };

            // 3. APPEL DU BON SERVICE (AddLieuAsync au lieu de AddTrajetAsync)
            if (await _db.AddLieuAsync(nouveau))
            {
                await Shell.Current.GoToAsync("..");
            }
            else
            {
                await Shell.Current.DisplayAlert("Erreur", "Impossible d'enregistrer le lieu.", "OK");
            }
        }

        [RelayCommand]
        private async Task Cancel()
        {
            await Shell.Current.GoToAsync("..");
        }
    }
}