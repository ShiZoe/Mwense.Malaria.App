using Mwense.Malaria.App.Models;
using Mwense.Malaria.App.ViewModels;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Mwense.Malaria.App.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PatientsListPage : ContentPage
    {
        //public ObservableCollection<string> Patients { get; set; }
        PatientsListViewModel _viewModel;

        public PatientsListPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = new PatientsListViewModel();
        }

        async void Handle_ClientSelected(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection != null)
            {
                Patients patient = (Patients)e.CurrentSelection.FirstOrDefault();
                await Shell.Current.GoToAsync($"{nameof(PatientDetailPage)}?{nameof(PatientDetailViewModel.OpdNumber)}={patient.OPDNumber}");
            }
            else
                return;

            ///await DisplayAlert("Item Tapped", "An item was tapped.", "OK");

            //Deselect Item
            //((ListView)sender).SelectedItem = null;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            // Retrieve all the notes from the database, and set them as the
            // data source for the CollectionView.
            MyListView.ItemsSource = await App.Database.GetPatientsAsync();
        }
    }
}
