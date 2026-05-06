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
    [QueryProperty(nameof(TrajetAModifier), "TrajetEchange")]
    // AJOUT : partial et : ObservableObject
    public partial class EditTrajetViewModel : ObservableObject
    {
        private readonly ConnexionBD _db;

        // AJOUT : Le champ pour recevoir l'objet à modifier
        [ObservableProperty]
        private Trajet _trajetAModifier;

        [ObservableProperty]
        private ObservableCollection<Lieu> _lieux;

        [ObservableProperty]
        private ObservableCollection<TypeTransport> _types;

        [ObservableProperty] private Lieu _selectedDepart;
        [ObservableProperty] private Lieu _selectedArrivee;
        [ObservableProperty] private TypeTransport _selectedType;
        [ObservableProperty] private decimal _prix;
        [ObservableProperty] private DateTime _dateDep;
        [ObservableProperty] private TimeSpan _heureDep;

        public EditTrajetViewModel(ConnexionBD db)
        {
            _db = db;
            // Ne pas oublier d'initialiser les listes pour les Pickers !
            ChargerDonnees();
        }

        private async void ChargerDonnees()
        {
            var villes = await _db.GetLieuxAsync();
            Lieux = new ObservableCollection<Lieu>(villes);

            var types = await _db.GetTypesAsync();
            Types = new ObservableCollection<TypeTransport>(types);
        }

        // CETTE MÉTHODE EST AUTOMATIQUE : Elle remplit les champs quand le trajet arrive
        partial void OnTrajetAModifierChanged(Trajet value)
        {
            if (value == null) return;

            // On remplit les champs simples
            Prix = value.TRA_PRIX;
            DateDep = value.TRA_DATEDEPART;
            HeureDep = value.TRA_HEUREDEPART;

            // --- CORRECTION DES PICKERS ---
            // On cherche dans la liste "Lieux" celui dont l'ID correspond à TRA_LIEU_DEPART_ID
            SelectedDepart = Lieux?.FirstOrDefault(l => l.LIE_ID == value.TRA_LIEU_DEPART_ID);

            // On cherche dans la liste "Lieux" celui dont l'ID correspond à TRA_LIEU_ARRIVEE_ID
            SelectedArrivee = Lieux?.FirstOrDefault(l => l.LIE_ID == value.TRA_LIEU_ARRIVEE_ID);

            // Pour le type, c'est déjà TYP_ID dans ton modèle
            SelectedType = Types?.FirstOrDefault(t => t.TYP_ID == value.TYP_ID);
        }

        [RelayCommand]
        private async Task Update()
        {
            if (TrajetAModifier == null || SelectedDepart == null || SelectedArrivee == null || SelectedType == null)
            {
                await Shell.Current.DisplayAlert("Erreur", "Veuillez remplir tous les champs", "OK");
                return;
            }

            // On utilise les noms exacts de ta classe Trajet.cs
            TrajetAModifier.TRA_LIEU_DEPART_ID = SelectedDepart.LIE_ID;
            TrajetAModifier.TRA_LIEU_ARRIVEE_ID = SelectedArrivee.LIE_ID;
            TrajetAModifier.TYP_ID = SelectedType.TYP_ID;
            TrajetAModifier.TRA_PRIX = Prix;

            // On sépare la date et l'heure car ton modèle a deux propriétés distinctes
            TrajetAModifier.TRA_DATEDEPART = DateDep.Date;
            TrajetAModifier.TRA_HEUREDEPART = HeureDep;

            // Pour l'arrivée, on peut mettre la même chose ou ajouter un décalage
            TrajetAModifier.TRA_DATEARRIVEE = DateDep.Date;
            TrajetAModifier.TRA_HEUREARRIVEE = HeureDep.Add(TimeSpan.FromHours(2));

            if (await _db.UpdateTrajetAsync(TrajetAModifier))
            {
                await Shell.Current.GoToAsync("..");
            }
        }

        [RelayCommand]
        private async Task Cancel() => await Shell.Current.GoToAsync("..");
    }
}