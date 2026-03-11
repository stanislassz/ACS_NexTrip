using ACS_NexTrip.Services; 
using Microsoft.Data.SqlClient;
using System.Data;

namespace ACS_NexTrip.Views;

public partial class RegisterPage : ContentPage
{
    public RegisterPage()
    {
        InitializeComponent();
    }

    // Cette méthode règle l'erreur sur 'OnInscriptionClicked'
    private async void OnInscriptionClicked(object sender, EventArgs e)
    {
        ConnexionBD MaBD = new ConnexionBD();

        try
        {
            MaBD.Ouvrir();

            using (SqlCommand cmd = new SqlCommand("ps_Inscription", MaBD.Connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                // On récupère les valeurs via les x:Name du XAML
                cmd.Parameters.AddWithValue("@Login", EntryLogin.Text);
                cmd.Parameters.AddWithValue("@Password", EntryPassword.Text);
                cmd.Parameters.AddWithValue("@Nom", EntryNom.Text);
                cmd.Parameters.AddWithValue("@Prenom", EntryPrenom.Text);
                cmd.Parameters.AddWithValue("@Email", EntryEmail.Text);

                int resultat = await cmd.ExecuteNonQueryAsync();

                if (resultat > 0)
                {
                    await DisplayAlert("Succès", "Inscription réussie !", "OK");
                    await Shell.Current.GoToAsync(".."); // Retour automatique au Login
                }
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Erreur", "Problème : " + ex.Message, "OK");
        }
        finally
        {
            MaBD.Fermer();
        }
    }

    // Cette méthode RÈGLE TON ERREUR ACTUELLE sur 'OnRetourClicked'
    private async void OnRetourClicked(object sender, EventArgs e)
    {
        // On demande au Shell de revenir à la page précédente
        await Shell.Current.GoToAsync("..");
    }
}