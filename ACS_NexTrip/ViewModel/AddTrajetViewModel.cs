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

        // ✅ On utilise TypeTransport, pas le type système "Type"
        [ObservableProperty]
        private ObservableCollection<TypeTransport> _types;

        [ObservableProperty]
        private TimeSpan _heureDep = DateTime.Now.TimeOfDay;

        [ObservableProperty]
        private TimeSpan _heureArr = DateTime.Now.TimeOfDay.Add(TimeSpan.FromHours(2));

        // ✅ selectedType est bien un TypeTransport
        [ObservableProperty]
        private TypeTransport _selectedType;

        // Ce que l'utilisateur sélectionne
        [ObservableProperty]
        private Lieu _selectedDepart;

        [ObservableProperty]
        private Lieu _selectedArrivee;

        [ObservableProperty]
        private decimal _prix;

        [ObservableProperty]
        private DateTime _dateDep = DateTime.Now;

        // ✅ On attend que la première soit finie avant de lancer la deuxième
        public async void ChargerDonnees()
        {
            var villes = await _db.GetLieuxAsync();
            Lieux = new ObservableCollection<Lieu>(villes);

            var types = await _db.GetTypesAsync();
            Types = new ObservableCollection<TypeTransport>(types);
        }

        public AddTrajetViewModel(ConnexionBD db)
        {
            _db = db;
            ChargerDonnees();
        }


        [RelayCommand]
        private async Task Save()
        {
            // Ajoute une petite vérification pour le type aussi
            if (SelectedDepart == null || SelectedArrivee == null || SelectedType == null)
            {
                await Shell.Current.DisplayAlert("Erreur", "Veuillez remplir tous les champs", "OK");
                return;
            }

            var nouveau = new Trajet
            {
                TRA_DATEDEPART = DateDep.Date, // On s'assure de ne prendre que la date
                TRA_DATEARRIVEE = DateDep.Date,

                // CORRECTION ICI : On utilise les variables bindées au TimePicker
                TRA_HEUREDEPART = HeureDep,
                TRA_HEUREARRIVEE = HeureArr,

                TRA_LIEU_DEPART_ID = SelectedDepart.LIE_ID,
                TRA_LIEU_ARRIVEE_ID = SelectedArrivee.LIE_ID,
                TYP_ID = SelectedType.TYP_ID,
                TRA_PRIX = Prix
            };

            if (await _db.AddTrajetAsync(nouveau))
            {
                await Shell.Current.GoToAsync("..");
            }
        }
    }
}