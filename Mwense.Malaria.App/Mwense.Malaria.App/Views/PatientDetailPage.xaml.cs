using Mwense.Malaria.App.Models;
using Mwense.Malaria.App.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Mwense.Malaria.App.Views
{
    public partial class PatientDetailPage : ContentPage
    {
        public PatientDetailPage()
        {
            InitializeComponent();
            BindingContext = new PatientDetailViewModel();
        }
    }
}