using ACS_NexTrip.Services; 
using Microsoft.Data.SqlClient;
using System.Data;

using ACS_NexTrip.ViewModel;

namespace ACS_NexTrip.Pages;

public partial class RegisterPage : ContentPage
{
    public RegisterPage(RegisterViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}