using Mwense.Malaria.App.Models;
using Mwense.Malaria.App.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;

namespace Mwense.Malaria.App.ViewModels
{
    class PatientsViewModel : BaseViewModel
    {
        public Action DisplayClientAlreadyRegisteredPrompt;
        public Action DisplayRegistrationSuccessPrompt;
        public Action DisplayRegistrationFailedPrompt;
        public Action DisplayRequiredFieldPrompt;
        public ActivityIndicator activity;

        //public event PropertyChangedEventHandler PropertyChanged = delegate { };


        private int Opdnumber { get; set; }
        public int OPDNumber
        {
            get { return Opdnumber; }
            set
            {
                Opdnumber = value;
                OnPropertyChanged("OPDNumber");
            }
        }
        private string Firstname { get; set; }
        public string FirstName
        {
            get { return Firstname; }
            set
            {
                Firstname = value;
                //PropertyChanged(this, new PropertyChangedEventArgs("FirstName"));
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
        private DateTime Dateofbirth { get; set; }
        public DateTime DateOfBirth 
        {
            get {return Dateofbirth.Date; }
            set
            {
                Dateofbirth = value;
                OnPropertyChanged("DateOfBirth");
            }
        }
        private string Housenumber { get; set; }
        public string HouseNumber 
        {
            get { return Housenumber.ToUpper(); }
            set
            {
                Housenumber = value.ToUpper();
                OnPropertyChanged("HouseNumber");
            }
        }
        private string village { get; set; }
        public string Village 
        {
            get { return village; }
            set
            {
                village = value;
                OnPropertyChanged("Village");
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

        public PatientsViewModel()
        {
            SubmitCommand = new Command(OnSubmitClicked);
            this.PropertyChanged += 
                (_, __) => SubmitCommand.ChangeCanExecute();
        }

        private async void OnSubmitClicked(object obj)
        {
            ActivityRunning();

            if (Opdnumber != 0 & Firstname != null & Lastname != null & Dateofbirth != null & village != null)
            {
                var model = new Patients
                {
                    OPDNumber = Opdnumber,
                    FirstName = Firstname.ToUpper(),
                    LastName = Lastname.ToUpper(),
                    DateOfBirth = Dateofbirth.Date,
                    HouseNumber = Housenumber,
                    Village = village.ToUpper(),
                    PhoneNumber = Phonenumber,
                };
                var check = await App.Database.CheckClientAsync(Opdnumber);
                if (check)
                {
                    DisplayClientAlreadyRegisteredPrompt();
                    Clear();
                    return;
                }
                else
                {
                    var responce = await App.Database.CreatePatientAsync(model);

                    if (responce)
                    {
                        ActivityStopped();
                        DisplayRegistrationSuccessPrompt();
                        Clear();
                        //await Shell.Current.GoToAsync("PatientsListPage");
                        await Shell.Current.GoToAsync($"//{nameof(PatientsListPage)}");
                        return;
                    }
                    else
                    {
                        ActivityStopped();
                        DisplayRegistrationFailedPrompt();
                        Clear();
                        return;
                    }
                        
                    //await Shell.Current.GoToAsync("..");
                }
            }
            else
            {
                ActivityStopped();
                DisplayRequiredFieldPrompt();
                return;
            }
                
        }
        public void Clear()
        {
            OPDNumber = 0;
            FirstName = string.Empty;
            LastName = string.Empty;
            DateOfBirth = DateTime.Today;
            HouseNumber = string.Empty;
            Village = string.Empty;
            PhoneNumber = string.Empty;
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
