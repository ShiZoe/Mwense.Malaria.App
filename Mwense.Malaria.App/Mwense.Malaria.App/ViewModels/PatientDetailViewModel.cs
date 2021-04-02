using Mwense.Malaria.App.Models;
using Mwense.Malaria.App.Views;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Xamarin.Forms;

namespace Mwense.Malaria.App.ViewModels
{
    [QueryProperty(nameof(OpdNumber), nameof(OpdNumber))]
    public class PatientDetailViewModel : BaseViewModel
    {
        private int opdNumber;
        private string firstName;
        private string lastName;
        private string village;
        private string dateOfBirth;
        public Command<Patients> ItemTapped { get; }

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

        public string DateOfBirth
        {
            get { return dateOfBirth; }
            set
            {
                dateOfBirth = value;
                OnPropertyChanged("DateOfBirth");
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
            }
        }

        public async void LoadPatientId(int opdNumber)
        {
            try
            {
                //int opdnum = Convert.ToInt32(opdNumber);
                Patients person = await App.Database.GetPatientAsync(opdNumber);
                OPDNumber = person.OPDNumber;
                FirstName = person.FirstName;
                LastName = person.LastName;
                Village = person.Village;
                DateOfBirth = person.DateOfBirth.ToString("dd/MM/yyyy");
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Item");
            }
        }
    }
}
