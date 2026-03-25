using ACS_NexTrip.Services;
using ACS_NexTrip.ViewModel;

namespace ACS_NexTrip.Pages;

public partial class TrajetPage : ContentPage
{
	public TrajetPage(TrajetViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel; // On lie le ViewModel à la page
    }
}