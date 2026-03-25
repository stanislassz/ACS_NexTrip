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

        // La liste qui sera liée au CollectionView dans le XAML
        [ObservableProperty]
        private ObservableCollection<Trajet> _trajets;

        public TrajetViewModel(ConnexionBD db)
        {
            // On charge les trajets dès l'initialisation
            _db = db;
            ChargerTrajets();
        }

        public async void ChargerTrajets()
        {
            Trajets = new ObservableCollection<Trajet>(await _db.GetTrajetsAsync());
        }

        [RelayCommand]
        private async Task GoToAddTrajet()
        {
            // On utilise Shell pour naviguer vers la page de création
            await Shell.Current.GoToAsync(nameof(Pages.AddTrajetPage));
        }

        [RelayCommand]
        public async Task Appearing()
        {
            // On appelle ta méthode de chargement
            await GetTrajets();
        }

        [RelayCommand]
        public async Task GetTrajets()
        {
            var data = await _db.GetTrajetsAsync();
            Trajets.Clear();
            foreach (var t in data)
            {
                Trajets.Add(t);
            }
        }


        [RelayCommand]
        public async Task DeleteTrajet(Trajet trajet)
        {
            if (trajet == null) return;

            // 1. On appelle le service de suppression
            bool success = await _db.DeleteTrajetAsync(trajet.TRA_ID);

            if (success)
            {
                // 2. On le retire de la liste pour que l'interface MAUI se mette à jour
                Trajets.Remove(trajet);
            }
            else
            {
                // 3. Message d'erreur si SQL refuse (ex: clé étrangère sur une réservation)
                await App.Current.MainPage.DisplayAlert("Erreur",
                    "Impossible de supprimer ce trajet. Vérifiez s'il n'est pas lié à d'autres données.",
                    "OK");
            }
        }
    }
}