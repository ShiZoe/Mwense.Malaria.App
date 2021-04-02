using Mwense.Malaria.App.Models;
using Mwense.Malaria.App.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using System.Threading;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Mwense.Malaria.App.ViewModels
{
    [QueryProperty(nameof(OpdNumber), nameof(OpdNumber))]
    class FollowupViewModel : BaseViewModel
    {
        CancellationTokenSource cts;
        private int ID { get; set; }
        private int opdNumber;
        private string firstName;
        private string lastName;
        private string village;
        private string testDate;
        private string phoneNumber;
        private DateTime reviewDate = DateTime.Today.AddDays(-2);
        private string latitude;
        private string longitude;
        private string Treatmentsuccess;
        public ActivityIndicator activity;

        public Action DisplayDeviceNotSupportedPrompt;
        public Action DisplayFeatureNotEnabledPrompt;
        public Action DisplayPemissionNotEnabledPrompt;
        public Action DisplaySystemErrorPrompt;
        public Action DisplayRequiredFeildsPrompt;
        public string FnxEx;
        public string FneEx;
        public string PEx;
        public string Ex;
        //public event PropertyChangedEventHandler PropertyChanged = delegate { };

        public Command GetCoordinates { get; }
        public Command ReviewPatient { get; }
        public int OPDNumber
        {
            get { return opdNumber; }
            set
            {
                opdNumber = value;
                OnPropertyChanged("OPDNumber");
            }
        }

        public string FirstName
        {
            get { return firstName; }
            set
            {
                firstName = value;
                OnPropertyChanged("FirstName");
            }
        }

        public string LastName
        {
            get { return lastName; }
            set
            {
                lastName = value;
                OnPropertyChanged("LastName");
            }
        }

        public string Village
        {
            get { return village; }
            set
            {
                village = value;
                OnPropertyChanged("Village");
            }
        } 

        public string TestDate
        {
            get { return testDate; }
            set
            {
                testDate = value;
                OnPropertyChanged("TestDate");
            }
        }
        public string PhoneNumber
        {
            get { return phoneNumber; }
            set
            {
                phoneNumber = value;
                OnPropertyChanged("PhoneNumber");
            }
        }

        public string Latitude
        {
            get { return latitude; }
            set
            {
                latitude = value;
                OnPropertyChanged("Latitude");
            }

        } 
        public string Longitude
        {
            get { return longitude; }
            set
            {
                longitude = value;
                OnPropertyChanged("Longitude");
            }
        }

    
        public string TreatmentSuccess
        {
            get { return Treatmentsuccess; }
            set
            {
                Treatmentsuccess = value;
                OnPropertyChanged("TreatmentSuccess");
            }
        }

        public int OpdNumber
        {
            get
            {
                return opdNumber;
            }
            set
            {
                opdNumber = value;
                LoadPatientId(value);
                OnPropertyChanged("OpdNumber");
            }
        }

        public FollowupViewModel()
        {
            GetCoordinates = new Command(GetCurrentLocation);
            ReviewPatient = new Command(SubmitReview);
        }

        public async void LoadPatientId(int opdNumber)
        {
            try
            {
                //int opdnum = Convert.ToInt32(opdNumber);
                //
                FollowUpList person = await App.Database.GetFollowupAsync(opdNumber, reviewDate);
                ID = person.ID;
                OPDNumber = person.OPDNumber;
                FirstName = person.FirstName;
                LastName = person.LastName;
                Village = person.Village;
                PhoneNumber = person.PhoneNumber;
                TestDate = person.TestDate.ToString("dd/MM/yyyy");
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Item");
            }
        }

        public void OnDisappearing()
        {
            if (cts != null && !cts.IsCancellationRequested)
                cts.Cancel();
            Clear();
            //base.OnDisappearing();
        }
        
        private async void SubmitReview(Object obj)
        {
            ActivityRunning();
            var model = new FollowUp()
            {
                OPDNumber = OPDNumber,
                CordLang = Latitude,
                CordLong = Longitude,
                TreatmentStatus = TreatmentSuccess,
                FollowupDate = DateTime.Today

            };
            if (latitude != null & Treatmentsuccess != null & longitude != null)
            {
                
                var query = await App.Database.SubmitReviewedAsync(model);
                var responce = await App.Database.UpdateFollowupAsync(ID);
                if (responce && query)
                {
                    ActivityStopped();
                    Clear();
                    await Shell.Current.GoToAsync($"//{nameof(FollowupListPage)}");
                    return;
                }
                else
                {
                    ActivityStopped();
                    DisplayRequiredFeildsPrompt();
                    return;
                }
            }
            else
            {
                ActivityStopped();
                DisplayRequiredFeildsPrompt();
                return;
            }

        }

        public void Clear()
        {
            OPDNumber = 0;
            Latitude = string.Empty;
            Longitude = string.Empty;
            TreatmentSuccess = string.Empty;
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

        private async void GetCurrentLocation(object obj)
        {
            ActivityRunning();

            try
            {
                var request = new GeolocationRequest(GeolocationAccuracy.Medium, TimeSpan.FromSeconds(10));
                cts = new CancellationTokenSource();
                var location = await Geolocation.GetLocationAsync(request, cts.Token);

                if (location != null)
                {
                    ActivityStopped();
                    Longitude = location.Longitude.ToString();
                    Latitude = location.Latitude.ToString();
                    return;
                    //Console.WriteLine($"Latitude: {location.Latitude}, Longitude: {location.Longitude}, Altitude: {location.Altitude}");
                }
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                FnxEx = fnsEx.ToString();
                // Handle not supported on device exception
                ActivityStopped();
                DisplayDeviceNotSupportedPrompt();
                return;
            }
            catch (FeatureNotEnabledException fneEx)
            {
                // Handle not enabled on device exception
                FneEx = fneEx.ToString();
                ActivityStopped();
                DisplayFeatureNotEnabledPrompt();
                return;
            }
            catch (PermissionException pEx)
            {
                // Handle permission exception
                PEx = pEx.ToString();
                ActivityStopped();
                DisplayPemissionNotEnabledPrompt();
                return;
            }
            catch (Exception ex)
            {
                // Unable to get location
                Ex = ex.ToString();
                ActivityStopped();
                DisplaySystemErrorPrompt();
                return;
            }
        }

    }
}
