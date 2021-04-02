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
    public partial class LoginPage : ContentPage
    {
        LoginViewModel _loginViewModel;
        public LoginPage()
        {
            //var vm = new LoginViewModel();
            //this.BindingContext = vm;
            InitializeComponent();
            BindingContext = _loginViewModel = new LoginViewModel();
            _loginViewModel.DisplayInvalidLoginPrompt += () => DisplayAlert("Login Failed", "Username or Password Incorrect", "OK");
            _loginViewModel.NoInternetConnection += () => DisplayAlert("Error", "No Internet Coonect Avaliable, Please Connect to the Internet and try again", "OK");
            _loginViewModel.DisplayUserDetailsPrompt += () => DisplayAlert("Error", "Please enter the username and password to continue", "OK");

            _loginViewModel.activity = activity;



            //popupLoadingView = vm.popupLoadingView;

            Username.Completed += (object sender, EventArgs e) =>
            {
                Password.Focus();
            };

            Password.Completed += (object sender, EventArgs e) =>
            {
                _loginViewModel.LoginCommand.Execute(null);
            };

        }
    }
}