using Mwense.Malaria.App.Models;
using Mwense.Malaria.App.Views;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Mwense.Malaria.App.ViewModels
{
    public class PatientsListViewModel : BaseViewModel
    {
        private Patients _selectedItem;
        public Command<Patients> ItemTapped { get; }

        public PatientsListViewModel()
        {
            Title = "Patients Registry";
            //LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            ItemTapped = new Command<Patients>(OnItemSelected);

            //AddItemCommand = new Command(OnAddItem);
        }
        public void OnAppearing()
        {
            IsBusy = true;
            SelectedItem = null;
        }

        public Patients SelectedItem
        {
            get => _selectedItem;
            set
            {
                SetProperty(ref _selectedItem, value);
                OnItemSelected(value);
            }
        }

        async void OnItemSelected(Patients patient)
        {
            if (patient == null)
                return;

            // This will push the ItemDetailPage onto the navigation stack
            await Shell.Current.GoToAsync($"{nameof(PatientDetailPage)}?{nameof(PatientDetailViewModel.OpdNumber)}={patient.OPDNumber}");
        }
    }
}
