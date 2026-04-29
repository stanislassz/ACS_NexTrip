using ACS_NexTrip.ViewModel;

namespace ACS_NexTrip.Pages;

public partial class LoginPage : ContentPage
{
    public LoginPage(LoginViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}