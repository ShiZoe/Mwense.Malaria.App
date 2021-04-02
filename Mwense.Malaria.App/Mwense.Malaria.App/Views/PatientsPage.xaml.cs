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
    public partial class PatientsPage : ContentPage
    {
        PatientsViewModel _viewModel;

        public PatientsPage()
        {
            //var vm = new PatientsViewModel();
            //this.BindingContext = vm;
            InitializeComponent();
            BindingContext = _viewModel = new PatientsViewModel();
            _viewModel.DisplayClientAlreadyRegisteredPrompt += () => DisplayAlert("Error", "The Patient is Aready Registered", "OK");
            _viewModel.DisplayRegistrationSuccessPrompt += () => DisplayAlert("Success", "The Patient has been registered successfully", "OK");
            _viewModel.DisplayRegistrationFailedPrompt += () => DisplayAlert("Error", "User registration failed, Please try again", "OK");
            _viewModel.DisplayRequiredFieldPrompt += () => DisplayAlert("Error", "OPD Number, First Name, Last Name and Village Fields are required", "OK");
            _viewModel.activity = activity;

            Opdnumber.Completed += (object sender, EventArgs e) =>
            {
                Firstname.Focus();
            };

            Firstname.Completed += (object sender, EventArgs e) =>
            {
                Lastname.Focus();
            };
            Housenumber.Completed += (object sender, EventArgs e) =>
            {
                Village.Focus();
            };
            Village.Completed += (object sender, EventArgs e) =>
            {
                Phonenumber.Focus();
            };
            Phonenumber.Completed += (object sender, EventArgs e) =>
            {
                _viewModel.SubmitCommand.Execute(null);
            };
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

    }
}