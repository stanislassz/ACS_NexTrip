using ACS_NexTrip.Services;
using ACS_NexTrip.ViewModel;

namespace ACS_NexTrip.Pages;

public partial class LieuPage : ContentPage
{
    LieuViewModel _viewModel;
    public LieuPage(LieuViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel; // On lie le ViewModel à la page
        _viewModel = viewModel; // On garde une référence pour les appels futurs
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        _viewModel.AppearingCommand.Execute(null); // On déclenche la commande pour rafraîchir les données
    }
}