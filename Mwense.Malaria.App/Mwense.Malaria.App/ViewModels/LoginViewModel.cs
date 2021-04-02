using Mwense.Malaria.App.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Mwense.Malaria.App.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {

        public Action DisplayInvalidLoginPrompt;
        public Action DisplayUserDetailsPrompt;
        public Action NoInternetConnection;
        public ActivityIndicator activity;
        //public event PropertyChangedEventHandler PropertyChanged = delegate { };
        //private Database _database = new Database();
        private string username;
        public string Username
        {
            get { return username; }
            set
            {
                username = value;
                //PropertyChanged(this, new PropertyChangedEventArgs("Username"));
                OnPropertyChanged("Username");
            }
        }
        private string password;

        public string Password
        {
            get { return password; }
            set
            {
                password = value;
                //PropertyChanged(this, new PropertyChangedEventArgs("Password"));
                OnPropertyChanged("Password");
            }
        }
        public Command LoginCommand { get; }

        public LoginViewModel()
        {
            LoginCommand = new Command(OnLoginClicked);
        }

        private bool CheckInternetConnection()
        {
            var current = Connectivity.NetworkAccess;
            if (current == NetworkAccess.None)
                return false;
            else
                return true;
        }

        private async void OnLoginClicked(object obj)
        {
            ActivityRunning();
            if (username != null && password != null)
            {
                if (username == "Mwensedho" & password == "Mwense12345")
                {
                    ActivityStopped();
                    Clear();
                    await Shell.Current.GoToAsync($"//{nameof(FollowupListPage)}");
                    return;
                }
                else
                {
                    ActivityStopped();
                    DisplayInvalidLoginPrompt();
                    Clear();
                    return;
                }
            }
            else
            {
                ActivityStopped();
                DisplayUserDetailsPrompt();
                return;
            }
                /*var responce = await App.Database.LoginAsync(username, password);
                //var responce = await _apiServices.LoginAsync(Username, Password);
                // Prefixing with `//` switches to a different navigation stack instead of pushing to the active one
                if (responce)
                {
                    ActivityStopped();
                    await Shell.Current.GoToAsync($"//{nameof(UsersPage)}");
                    return;
                }
                else
                {
                    ActivityStopped();
                    DisplayInvalidLoginPrompt();
                    Clear();
                    return;
                }
                    
            }
            else
            {
                ActivityStopped();
                DisplayUserDetailsPrompt();
                return;
            }*/
                
            
        }

        public void Clear()
        {
            
            Username = String.Empty;
            Password = string.Empty;
        }

        public void ActivityRunning()
        {
            activity.IsEnabled = true;
            activity.IsRunning = true;
            activity.IsVisible = true;
        }

        public void ActivityStopped()
        {
            activity.IsEnabled = false;
            activity.IsRunning = false;
            activity.IsVisible = false;
        }
       
    }
}
