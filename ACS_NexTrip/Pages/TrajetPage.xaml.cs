using ACS_NexTrip.Services;
using ACS_NexTrip.ViewModel;

namespace ACS_NexTrip.Pages;

public partial class TrajetPage : ContentPage
{
    // On déclare le ViewModel pour y accéder plus facilement
    private readonly ViewModel.TrajetViewModel _viewModel;
    public TrajetPage(TrajetViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel; // On lie le ViewModel à la page
    }
}