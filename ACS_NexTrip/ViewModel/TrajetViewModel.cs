using System.Collections.ObjectModel;
using ACS_NexTrip.Models;
using ACS_NexTrip.Services;
using CommunityToolkit.Mvvm.ComponentModel;

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
    }
}