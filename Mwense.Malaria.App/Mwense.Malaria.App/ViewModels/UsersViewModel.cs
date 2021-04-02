using Mwense.Malaria.App.Models;
using Mwense.Malaria.App.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;

namespace Mwense.Malaria.App.ViewModels
{
    class UsersViewModel : BaseViewModel
    {
        public Action DisplaySaveSucessPrompt;
        public Action DisplaySaveFailedPrompt;
        public Action DisplayUserExistPrompt;
        public Action DisplayRequriedFieldPrompt;
        //public event PropertyChangedEventHandler PropertyChanged = delegate { };

        private string Username;
        public string UserName 
        {
            get { return Username; }
            set
            {
                Username = value;
                OnPropertyChanged("UserName");
            }
        }
        private string Password { get; set; }
        public string PassWord 
        {
            get { return Password; }
            set
            {
                Password = value;
                OnPropertyChanged("PassWord");
            }
        }

        private string Firstname { get; set; }
        public string FirstName 
        {
            get { return Firstname; }
            set
            {
                Firstname = value;
                OnPropertyChanged("FirstName");
            }
        }

        private string Lastname { get; set; }
        public string LastName 
        {
            get { return Lastname; }
            set
            {
                Lastname = value;
                OnPropertyChanged("LastName");
            }
        }
        private string Phonenumber { get; set; }
        public string PhoneNumber
        {
            get { return Phonenumber; }
            set
            {
                Phonenumber = value;
                OnPropertyChanged("PhoneNumber");
            }
        }

        public Command SubmitCommand { get; }

        public UsersViewModel()
        {
            SubmitCommand = new Command(OnSubmitClicked);
        }

        private async void OnSubmitClicked(object obj)
        {
            var model = new Users
            {
                Username = Username,
                Password = Password,
                Firstname = Firstname.ToUpper(),
                Lastname = Lastname.ToUpper(),
                Phonenumber = Phonenumber.ToUpper(),
                DateCreated = DateTime.Now,
                DateModified =DateTime.Now,
            };

            if (Username != null && Password != null && Firstname != null && Lastname != null)
            {
                var check = await App.Database.CheckUserAsync(Username);

                if (check)
                {
                    DisplayUserExistPrompt();
                    Clear();
                    await Shell.Current.GoToAsync($"//{nameof(PatientsListPage)}");


                }
                else
                {
                    var responce = await App.Database.CreateUserAsync(model);
                    if (responce)
                    {
                        //();
                        Clear();
                        await Shell.Current.GoToAsync($"//{nameof(PatientsListPage)}");
                    }
                    else
                        Clear();
                    DisplaySaveFailedPrompt();
   
                }
   
            }
            else
                DisplayRequriedFieldPrompt();
                
        }

        private void Clear()
        {
            UserName = string.Empty;
            PassWord = string.Empty;
            FirstName = string.Empty;
            LastName = string.Empty;
            PhoneNumber = string.Empty;
        }

    }
}
