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
    }
}