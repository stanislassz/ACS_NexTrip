namespace ACS_NexTrip.Views;

public partial class HomePage : ContentPage
{
	public HomePage()
	{
		InitializeComponent();
	}

    private async void OnTrajetsClicked(object sender, EventArgs e)
    {
        // On navigue vers la page des trajets
        // Assure-toi que le nom de ta classe est bien TrajetPage
        await Navigation.PushAsync(new TrajetPage());
    }

    private async void OnUtilisateurClicked(object sender, EventArgs e)
    {
        // On navigue vers la page des trajets
        // Assure-toi que le nom de ta classe est bien TrajetPage
        await Navigation.PushAsync(new SettingsPage());
    }
}