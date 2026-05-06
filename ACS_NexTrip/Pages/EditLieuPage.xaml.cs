using ACS_NexTrip.ViewModel;

namespace ACS_NexTrip.Pages;

public partial class EditLieuPage : ContentPage
{
	public EditLieuPage(EditLieuViewModel vm)
	{
		InitializeComponent();
        BindingContext = vm;
    }
}