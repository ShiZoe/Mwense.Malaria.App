using Mwense.Malaria.App.Models;
using Mwense.Malaria.App.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;

namespace Mwense.Malaria.App.ViewModels
{
    public class LabResultsViewModel : BaseViewModel
    {
        public Action DisplaySaveSucessPrompt;
        public Action DisplaySaveFailedPrompt;
        public Action DisplayRequiredFieldsPrompt;
        public Action NoInternetConnection;
        public Action DisplayPatientNotRegisteredPrompt;
        public ActivityIndicator activity;


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
        private string Malariaresults { get; set; }
        public string MalariaResults
        {
            get { return Malariaresults; }
            set
            {
                Malariaresults = value;
                OnPropertyChanged("MalariaResults");
            }
        }

        public Command SubmitCommand { get; }
        public RadioButton Check1 { get; internal set; }
        public RadioButton Check2 { get; internal set; }

        public LabResultsViewModel()
        {
            SubmitCommand = new Command(OnSubmitClicked);
        }

        private async void OnSubmitClicked(object obj)
        {
            ActivityRunning();
            var model = new LabResults
            {
                OPDNumber = Opdnumber,
                MalariaResults = Malariaresults,
                TestDate = DateTime.Today
            };


            if (Opdnumber != 0 & Malariaresults != null)
            {
                var check = await App.Database.CheckClientAsync(Opdnumber);

                if (check)
                {
                    var client = await App.Database.GetPatientAsync(Opdnumber);
                    var followupmodel = new FollowUpList
                    {
                        OPDNumber = client.OPDNumber,
                        FirstName = client.FirstName,
                        LastName = client.LastName,
                        DateOfBirth = client.DateOfBirth,
                        Village = client.Village,
                        HouseNumber = client.HouseNumber,
                        PhoneNumber = client.PhoneNumber,
                        MalariaResults = Malariaresults,
                        TestDate = DateTime.Today,
                        IsReviewed = false
                    };



                    //var responce = await _apiServices.LoginAsync(Username, Password);
                    // Prefixing with `//` switches to a different navigation stack instead of pushing to the active one
                    var responce = await App.Database.SubmitResultsAsync(model);
                    var follows = await App.Database.SubmitFollowupAsync(followupmodel);


                    if (responce && follows)
                    {
                        // popupLoadingView.IsVisible = false;
                        ActivityStopped();
                        DisplaySaveSucessPrompt();
                        Clear();
                        //await Shell.Current.GoToAsync($"//{nameof(LabResultsPage)}");
                        //await Shell.Current.GoToAsync(nameof(LabResultsPage));
                    }
                    else
                    {
                        ActivityStopped();
                        DisplaySaveFailedPrompt();
                        Clear();
                        return;
                    }
                    //popupLoadingView.IsVisible = false;
                    //DisplayInvalidLoginPrompt();
                }
                else
                {
                    ActivityStopped();
                    Clear();
                    DisplayPatientNotRegisteredPrompt();
                    await Shell.Current.GoToAsync($"//{nameof(PatientsPage)}");
                    return;
                }
            }
            else
            {
                ActivityStopped();
                DisplayRequiredFieldsPrompt();
                return;
            }
                
            //await Shell.Current.GoToAsync($"//{nameof(PatientsListPage)}");


        }

        public void Clear()
        {
            OPDNumber = 0;
            MalariaResults = string.Empty;
            Check1.IsChecked = false;
            Check2.IsChecked = false;
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
