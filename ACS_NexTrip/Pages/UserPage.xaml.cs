using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ACS_NexTrip.ViewModel;

namespace ACS_NexTrip.Pages
{
    public partial class UserPage : ContentPage
    {
        public UserPage(UserViewModel vm) 
        {
            InitializeComponent();
            BindingContext = vm;
        }
    }
}