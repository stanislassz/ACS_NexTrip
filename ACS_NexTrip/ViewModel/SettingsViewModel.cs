using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ACS_NexTrip.Models;
using ACS_NexTrip.Services;
using ACS_NexTrip.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace ACS_NexTrip.ViewModel
{
    public partial class SettingsViewModel : ObservableObject
    {
        [ObservableProperty]
        private string _UTI_NOM;

        [ObservableProperty]
        private string _UTI_PRENOM;

        [ObservableProperty]
        private string _UTI_EMAIL;

        [ObservableProperty]
        private string _UTI_CP;

        [ObservableProperty]
        private string _UTI_TEL;

        [ObservableProperty]
        private string _UTI_ADRESSE;

        [ObservableProperty]
        private DateTime _DATE_NAISSANCE;

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

        [RelayCommand]
        private async Task Save(Utilisateur utilisateur)
        {
            if (utilisateur == null) return;

            try
            {
                // On appelle la méthode de mise à jour de ton service ConnexionBD
                bool isSuccess = await _db.UpdateUtilisateurAsync(utilisateur);

                if (isSuccess)
                {
                    await Shell.Current.DisplayAlert("Succès", "Vos informations ont été enregistrées.", "OK");
                }
                else
                {
                    await Shell.Current.DisplayAlert("Erreur", "Impossible de mettre à jour le profil.", "OK");
                }
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Erreur", ex.Message, "OK");
            }
        }
    }
}
