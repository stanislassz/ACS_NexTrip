namespace ACS_NexTrip.Pages;

public partial class HomePage : ContentPage
{
	public HomePage()
	{
		InitializeComponent();
        BindingContext = new ACS_NexTrip.ViewModel.HomeViewModel();
    }
}