using ACS_NexTrip.Services;
using ACS_NexTrip.ViewModel;

namespace ACS_NexTrip.Pages
{
    public partial class SettingsPage : ContentPage
    {
        public SettingsPage(SettingsViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }
    }
}
