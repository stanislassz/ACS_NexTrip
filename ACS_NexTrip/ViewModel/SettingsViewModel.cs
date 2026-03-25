using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using ACS_NexTrip.Services;
using ACS_NexTrip.Models;

namespace ACS_NexTrip.ViewModel
{
    public partial class SettingsViewModel : ObservableObject
    {
        private readonly ConnexionBD _db;

        // La liste qui sera liée au CollectionView dans le XAML
        [ObservableProperty]
        private ObservableCollection<Utilisateur> _utilisateurs;

        public SettingsViewModel(ConnexionBD db)
        {
            // On charge les trajets dès l'initialisation
            _db = db;
            ChargerUtilisateur();
        }

        public async void ChargerUtilisateur()
        {
            Utilisateurs = new ObservableCollection<Utilisateur>(await _db.GetUtilisateurAsync());
        }
    }
}
