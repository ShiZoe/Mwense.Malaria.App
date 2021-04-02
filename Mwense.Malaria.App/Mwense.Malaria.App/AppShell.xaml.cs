using Mwense.Malaria.App.ViewModels;
using Mwense.Malaria.App.Views;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Mwense.Malaria.App
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(PatientDetailPage), typeof(PatientDetailPage));
            Routing.RegisterRoute(nameof(FollowupDetailPage), typeof(FollowupDetailPage));
            CurrentItem = Login;
            //Routing.RegisterRoute(nameof(NewItemPage), typeof(NewItemPage));
        }

        private async void OnMenuItemClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//LoginPage");
        }
    }
}
