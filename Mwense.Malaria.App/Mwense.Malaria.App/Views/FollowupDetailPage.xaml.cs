using Mwense.Malaria.App.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Mwense.Malaria.App.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FollowupDetailPage : ContentPage
    {
        FollowupViewModel _followupViewModel;
        public FollowupDetailPage()
        {
            InitializeComponent();
            BindingContext = _followupViewModel = new FollowupViewModel();
            _followupViewModel.DisplayRequiredFeildsPrompt += () => DisplayAlert("Error", "Please Make sure to load coordinates and Enter Treatment Status", "OK");
            _followupViewModel.DisplayDeviceNotSupportedPrompt += () => DisplayAlert("Error", $"The Device Does Not Support GPRS Hence this feature is not supported {_followupViewModel.FnxEx}", "OK");
            _followupViewModel.DisplaySystemErrorPrompt += () => DisplayAlert("Error", $"The System crushed and failed please close and open application and try again. If it continues please email the developer {_followupViewModel.FneEx}", "OK");
            _followupViewModel.DisplayFeatureNotEnabledPrompt += () => DisplayAlert("Error", $"GPRS Location is disabled on the Device, Please enable GPRS Location and try again{_followupViewModel.PEx}", "OK");
            _followupViewModel.DisplayPemissionNotEnabledPrompt += () => DisplayAlert("Error", $"Please allow the application to use device GPRS services to getthe loation {_followupViewModel.Ex}", "OK");
            _followupViewModel.activity = activity;
        }

        protected override void OnDisappearing()
        {
            _followupViewModel.OnDisappearing();
            Clear();
            base.OnDisappearing();
        }

        public void Clear()
        {
            nop.IsChecked = false;
            yes.IsChecked = false;        }

    }
}