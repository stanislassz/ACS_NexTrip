namespace ACS_NexTrip.Pages;

public partial class AddTrajetPage : ContentPage
{
    public AddTrajetPage()
    {
        InitializeComponent();
        // On passe l'instance de la BD au ViewModel
        BindingContext = new ViewModel.AddTrajetViewModel(new Services.ConnexionBD());
    }
}