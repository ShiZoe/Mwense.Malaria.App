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
    public partial class UsersPage : ContentPage
    {
        public UsersPage()
        {
            var vm = new UsersViewModel();
            this.BindingContext = vm;
            vm.DisplaySaveSucessPrompt += () => DisplayAlert("Success", "New User has been registered successfully", "OK");
            vm.DisplayUserExistPrompt += () => DisplayAlert("Error", "The username has already been registered", "OK");
            vm.DisplaySaveFailedPrompt += () => DisplayAlert("Error", "User registration failed, Please try again", "OK");
            vm.DisplayRequriedFieldPrompt += () => DisplayAlert("Error", "User, Password, Firstname and Lastname Fields are required", "OK");
            InitializeComponent();

            Username.Completed += (object sender, EventArgs e) =>
            {
                Password.Focus();
            };

            Password.Completed += (object sender, EventArgs e) =>
            {
                Firstname.Focus();
            };

            Firstname.Completed += (object sender, EventArgs e) =>
            {
                Lastname.Focus();
            };

            Lastname.Completed += (object sender, EventArgs e) =>
            {
                Phonenumber.Focus();
            };
            Phonenumber.Completed += (object sender, EventArgs e) =>
            {
                vm.SubmitCommand.Execute(null);
            };
        }
    }
}