using ACS_NexTrip.Services;

namespace ACS_NexTrip.Views;

public partial class TrajetPage : ContentPage
{
	public TrajetPage()
	{
		InitializeComponent();
	}

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        // On instancie ta classe de connexion
        var service = new ConnexionBD();

        // On récupère les données
        var result = await service.GetTrajetsAsync();

        // On les donne au tableau
        TrajetsCollection.ItemsSource = result;
    }
}