using ACS_NexTrip.Models;
using ACS_NexTrip.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace ACS_NexTrip.ViewModel
{
    public partial class RegisterViewModel : ObservableObject
    {
        private readonly ConnexionBD _db;

        [ObservableProperty]
        private string _nom = string.Empty;

        [ObservableProperty]
        private string _prenom = string.Empty;

        [ObservableProperty]
        private string _login = string.Empty;

        [ObservableProperty]
        private string _password = string.Empty;

        [ObservableProperty]
        private string _email = string.Empty;

        public RegisterViewModel(ConnexionBD db)
        {
            _db = db;
        }

        // Génère "RegisterCommand"
        [RelayCommand]
        private async Task Register()
        {
            if (string.IsNullOrWhiteSpace(Nom) || string.IsNullOrWhiteSpace(Prenom) ||
                string.IsNullOrWhiteSpace(Login) || string.IsNullOrWhiteSpace(Password))
            {
                await App.Current.MainPage.DisplayAlert("Attention", "Tous les champs sont obligatoires.", "OK");
                return;
            }

            var u = new Utilisateur
            {
                UTI_NOM = Nom,
                UTI_PRENOM = Prenom,
                UTI_LOGIN = Login,
                UTI_PASSWORD = Password,
                UTI_EMAIL = Email
            };

            bool success = await _db.InscrireUtilisateurAsync(u);

            if (success)
            {
                await App.Current.MainPage.DisplayAlert("Succès", "Inscription terminée !", "OK");
                await Shell.Current.GoToAsync("..");
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Erreur", "L'inscription a échoué.", "OK");
            }
        }

        // Génère "GoBackCommand"
        [RelayCommand]
        private async Task GoBack() =>
            await Shell.Current.GoToAsync("..");
    }
}