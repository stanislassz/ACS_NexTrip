using ACS_NexTrip.Services; // Pour utiliser ta classe ConnexionBD
using Microsoft.Data.SqlClient; // Pour les commandes SQL
using System.Data;

namespace ACS_NexTrip.Views;

public partial class LoginPage : ContentPage
{
    public LoginPage()
    {
        InitializeComponent();
    }

    
    private async void OnGoToRegisterClicked(object sender, EventArgs e)
    {
        // Navigation vers la page d'inscription
        await Shell.Current.GoToAsync("RegisterPage");
    }

    
    private async void OnConnexionClicked(object sender, EventArgs e)
    {
        // On instancie ta classe de connexion avec SqlConnectionStringBuilder
        ConnexionBD MaBD = new ConnexionBD();

        try
        {
            MaBD.Ouvrir();

            // On utilise la procédure stockée ps_Connexion
            using (SqlCommand cmd = new SqlCommand("ps_Connexion", MaBD.Connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                // On récupère les textes des Entry via leur x:Name du XAML
                cmd.Parameters.AddWithValue("@Login", EntryLogin.Text);
                cmd.Parameters.AddWithValue("@Password", EntryPassword.Text);

                using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                {
                    if (reader.Read())
                    {
                        // Si l'utilisateur existe
                        // await DisplayAlert("Succès", "Connexion réussie !", "OK");
                        await Shell.Current.GoToAsync("/HomePage");
                    }
                    else
                    {
                        await DisplayAlert("Erreur", "Identifiants incorrects.", "OK");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Erreur Technique", ex.Message, "OK");
        }
        finally
        {
            MaBD.Fermer();
        }
    }
}