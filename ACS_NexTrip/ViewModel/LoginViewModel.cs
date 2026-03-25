using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ACS_NexTrip.Services; 
using CommunityToolkit.Mvvm.ComponentModel;

namespace ACS_NexTrip.ViewModel
{
    public class LoginViewModel : BindableObject
    {
        private readonly ConnexionBD _db = new ConnexionBD();

        // 1. Les propriétés pour les saisies (Binding Text)
        public string Login { get; set; }
        public string Password { get; set; }

        // 2. Déclaration des commandes (Binding Command)
        public Command ConnexionCommand { get; }
        public Command GoToRegisterCommand { get; } // C'est cette ligne qu'il manquait !

        public LoginViewModel()
        {
            // 3. Initialisation de la commande pour l'inscription
            GoToRegisterCommand = new Command(async () =>
            {
                await Shell.Current.GoToAsync("RegisterPage");
            });

            // 4. Initialisation de la commande pour la connexion
            ConnexionCommand = new Command(async () =>
            {
                if (string.IsNullOrWhiteSpace(Login) || string.IsNullOrWhiteSpace(Password))
                {
                    await App.Current.MainPage.DisplayAlert("Attention", "Champs vides !", "OK");
                    return;
                }

                bool isOk = await _db.VerifierConnexionAsync(Login, Password);

                if (isOk)
                {
                    await Shell.Current.GoToAsync("HomePage");
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert("Erreur", "Identifiants incorrects.", "OK");
                }
            });
        }
    }
}
