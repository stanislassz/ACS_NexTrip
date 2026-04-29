using ACS_NexTrip.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace ACS_NexTrip.ViewModel
{
    public partial class LoginViewModel : ObservableObject
    {
        private readonly ConnexionBD _db;

        [ObservableProperty]
        private string _login = string.Empty;

        [ObservableProperty]
        private string _password = string.Empty;

        public LoginViewModel(ConnexionBD db)
        {
            _db = db;
        }

        // Génère "ConnexionCommand" utilisable dans le XAML
        [RelayCommand]
        private async Task Connexion()
        {
            if (string.IsNullOrWhiteSpace(Login) || string.IsNullOrWhiteSpace(Password))
            {
                await App.Current.MainPage.DisplayAlert("Attention", "Champs vides !", "OK");
                return;
            }

            bool isOk = await _db.VerifierConnexionAsync(Login, Password);

            if (isOk)
                await Shell.Current.GoToAsync("HomePage");
            else
                await App.Current.MainPage.DisplayAlert("Erreur", "Identifiants incorrects.", "OK");
        }

        // Génère "GoToRegisterCommand"
        [RelayCommand]
        private async Task GoToRegister() =>
            await Shell.Current.GoToAsync("RegisterPage");
    }
}