using ACS_NexTrip.ViewModel;

namespace ACS_NexTrip.Pages;

public partial class EditTrajetPage : ContentPage
{
	public EditTrajetPage(EditTrajetViewModel vm)
	{
		InitializeComponent();
        BindingContext = vm;
    }
}