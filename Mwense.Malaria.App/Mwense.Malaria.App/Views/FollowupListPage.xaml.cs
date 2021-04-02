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
    public partial class FollowupListPage : ContentPage
    {
        //public ObservableCollection<string> Items { get; set; }
        private string results = "Postive";
        private DateTime date = DateTime.Today.AddDays(-2);
        public FollowupListPage()
        {
            InitializeComponent();

        }

        async void Handle_ClientSelected(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection != null)
            {
                FollowUpList followupList = (FollowUpList)e.CurrentSelection.FirstOrDefault();
                await Shell.Current.GoToAsync($"{nameof(FollowupDetailPage)}?{nameof(FollowupViewModel.OpdNumber)}={followupList.OPDNumber}");
            }
            else
                return;
        }


        protected override async void OnAppearing()
        {
            base.OnAppearing();
            IsBusy = true;
            MyListView.ItemsSource = await App.Database.FollowUpAsync(results, date);
            IsBusy = false;
        }


    }
}
