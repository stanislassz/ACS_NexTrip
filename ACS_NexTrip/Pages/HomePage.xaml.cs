using ACS_NexTrip.ViewModel;

namespace ACS_NexTrip.Pages;

public partial class HomePage : ContentPage
{
    public HomePage(HomeViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}