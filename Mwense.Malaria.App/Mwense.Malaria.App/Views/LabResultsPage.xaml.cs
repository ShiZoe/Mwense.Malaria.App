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
    public partial class LabResultsPage : ContentPage
    {
        LabResultsViewModel _labResultsViewModel;
        public LabResultsPage()
        {
            //var vm = new LabResultsViewModel();
            //this.BindingContext = vm;
            InitializeComponent();
            BindingContext = _labResultsViewModel = new LabResultsViewModel();
            _labResultsViewModel.DisplaySaveFailedPrompt += () => DisplayAlert("Error", "Client Records have not been saved, Please try again", "OK");
            _labResultsViewModel.DisplaySaveSucessPrompt += () => DisplayAlert("Success", "Pateint Results have been saved successfully", "OK");
            _labResultsViewModel.DisplayRequiredFieldsPrompt += () => DisplayAlert("Error", "Patient OPD Number and Malaria Results are required", "OK");
            _labResultsViewModel.DisplayPatientNotRegisteredPrompt += () => DisplayAlert("Error", "Patient Not Found in System, please register patient first", "OK");
            _labResultsViewModel.activity = activity;
            _labResultsViewModel.Check1 = neg;
            _labResultsViewModel.Check2 = pos;
            

            //popupLoadingView = vm.popupLoadingView;
        }

        //RadioButton appleRadioButton = new RadioButton { Content = "Apple" };\
        protected override void OnDisappearing()
        {
            //_followupViewModel.OnDisappearing();
            ClearBox();
            base.OnDisappearing();
        }

        public void ClearBox()
        {
            neg.IsChecked = false;
            pos.IsChecked = false;
        }

    }
}