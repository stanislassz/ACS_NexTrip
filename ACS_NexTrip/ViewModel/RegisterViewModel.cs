using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ACS_NexTrip.Services;
using ACS_NexTrip.Models;

namespace ACS_NexTrip.ViewModel
{
    public class RegisterViewModel : BindableObject
    {
        private readonly ConnexionBD _db = new ConnexionBD();

        // On ne garde que les 4 propriétés nécessaires
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }

        public Command RegisterCommand { get; }
        public Command GoBackCommand { get; }

        public RegisterViewModel()
        {
            RegisterCommand = new Command(async () =>
            {
                // Vérification : tous les champs doivent être remplis
                if (string.IsNullOrWhiteSpace(Nom) || string.IsNullOrWhiteSpace(Prenom) ||
                    string.IsNullOrWhiteSpace(Login) || string.IsNullOrWhiteSpace(Password))
                {
                    await App.Current.MainPage.DisplayAlert("Attention", "Tous les champs sont obligatoires.", "OK");
                    return;
                }

                // On crée l'objet utilisateur avec les 4 infos
                var u = new Utilisateur
                {
                    UTI_NOM = Nom,
                    UTI_PRENOM = Prenom,
                    UTI_LOGIN = Login,
                    UTI_PASSWORD = Password,
                    UTI_EMAIL = Email
                };

                // Appel de la PS via le service
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
            });

            GoBackCommand = new Command(async () => await Shell.Current.GoToAsync(".."));
        }
    }
}
