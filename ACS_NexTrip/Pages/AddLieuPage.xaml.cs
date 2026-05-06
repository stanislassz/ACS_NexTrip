namespace ACS_NexTrip.Pages;

public partial class AddLieuPage : ContentPage
{
	public AddLieuPage()
	{
		InitializeComponent();
        BindingContext = new ViewModel.AddLieuViewModel(new Services.ConnexionBD());
    }
}