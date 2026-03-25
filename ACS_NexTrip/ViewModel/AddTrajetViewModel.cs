using System.Collections.ObjectModel;
using ACS_NexTrip.Models;
using ACS_NexTrip.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace ACS_NexTrip.ViewModel
{
    public partial class AddTrajetViewModel : ObservableObject
    {
        private readonly ConnexionBD _db;

        // La liste des villes pour le Picker
        [ObservableProperty]
        private ObservableCollection<Lieu> _lieux;

        // Ce que l'utilisateur sélectionne
        [ObservableProperty]
        private Lieu _selectedDepart;

        [ObservableProperty]
        private Lieu _selectedArrivee;

        [ObservableProperty]
        private decimal _prix;

        [ObservableProperty]
        private DateTime _dateDep = DateTime.Now;

        public AddTrajetViewModel(ConnexionBD db)
        {
            _db = db;
            ChargerVilles();
        }

        public async void ChargerVilles()
        {
            // On récupère la liste via ton service
            var data = await _db.GetLieuxAsync();
            Lieux = new ObservableCollection<Lieu>(data);
        }

        [RelayCommand]
        private async Task Save()
        {
            if (SelectedDepart == null || SelectedArrivee == null) return;

            var nouveau = new Trajet
            {
                TRA_DATEDEPART = DateDep,
                TRA_DATEARRIVEE = DateDep,
                TRA_HEUREDEPART = DateTime.Now.TimeOfDay,
                TRA_HEUREARRIVEE = DateTime.Now.TimeOfDay.Add(TimeSpan.FromHours(2)),
                TRA_LIEU_DEPART_ID = SelectedDepart.LIE_ID, // On utilise l'ID de l'objet choisi
                TRA_LIEU_ARRIVEE_ID = SelectedArrivee.LIE_ID,
                TYP_ID = 1,
                TRA_PRIX = Prix
            };

            if (await _db.AddTrajetAsync(nouveau))
            {
                await Shell.Current.GoToAsync("..");
            }
        }
    }
}