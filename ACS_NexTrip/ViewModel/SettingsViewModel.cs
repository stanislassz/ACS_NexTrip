using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ACS_NexTrip.Services;
using ACS_NexTrip.Models;
using ACS_NexTrip.Services;
using CommunityToolkit.Mvvm.ComponentModel;

namespace ACS_NexTrip.ViewModel
{
    public partial class SettingsViewModel : ObservableObject
    {
        // Commandes pour les bindings du XAML
        public ICommand NavigateToDashboardCommand { get; }
        public ICommand NavigateToTripsCommand { get; }
        public ICommand NavigateToUsersCommand { get; }
        public ICommand ShowNotificationsCommand { get; }
        public ICommand ShowProfileCommand { get; }

        private readonly ConnexionBD _db;

        // La liste qui sera liée au CollectionView dans le XAML
        [ObservableProperty]
        private ObservableCollection<Utilisateur> _utilisateurs;

        public SettingsViewModel(ConnexionBD db)
        {
            // Initialisation des commandes
            NavigateToDashboardCommand = new Command(async () => await GoToHomeAsync());
            NavigateToTripsCommand = new Command(async () => await GoToTripsAsync());
            NavigateToUsersCommand = new Command(async () => await GoToSettingsAsync());
            ShowNotificationsCommand = new Command(() => { /* À implémenter */ });
            ShowProfileCommand = new Command(() => { /* À implémenter */ });

            // On charge les trajets dès l'initialisation
            _db = db;
            ChargerUtilisateur();
        }

        public async void ChargerUtilisateur()
        {
            Utilisateurs = new ObservableCollection<Utilisateur>(await _db.GetUtilisateurAsync());
        }

        private async Task GoToTripsAsync()
        {
            await Shell.Current.GoToAsync("TrajetPage");
        }

        private async Task GoToSettingsAsync()
        {
            await Shell.Current.GoToAsync("SettingsPage");
        }
        private async Task GoToHomeAsync()
        {
            await Shell.Current.GoToAsync("HomePage");
        }
    }
}
