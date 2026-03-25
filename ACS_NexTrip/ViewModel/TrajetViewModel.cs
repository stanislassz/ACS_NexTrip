using System.Collections.ObjectModel;
using ACS_NexTrip.Models;
using ACS_NexTrip.Services;

namespace ACS_NexTrip.ViewModel
{
    public class TrajetViewModel : BindableObject
    {
        private readonly ConnexionBD _db = new ConnexionBD();

        // La liste qui sera liée au CollectionView dans le XAML
        public ObservableCollection<Trajet> Trajets { get; set; } = new ObservableCollection<Trajet>();

        public TrajetViewModel()
        {
            // On charge les trajets dès l'initialisation
            ChargerTrajets();
        }

        public async void ChargerTrajets()
        {
            Trajets.Clear(); // On vide la liste actuelle
            var result = await _db.GetTrajetsAsync();

            foreach (var t in result)
            {
                Trajets.Add(t);
            }
        }
    }
}