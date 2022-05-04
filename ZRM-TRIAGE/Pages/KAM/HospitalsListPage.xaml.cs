using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ZRM_TRIAGE
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HospitalsListPage : ContentPage
    {
        public HospitalsListPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            HospitalsListViewModel hospitalsListViewModel = new HospitalsListViewModel();

            var list = hospitalsListViewModel.GetAll();
            BindingContext = list;
            HospitalListXAML.ItemsSource = list;
        }

        private async void AddAmbulanceClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddHospitalPage());
        }

        private async void HospitalInfoButtonClicked(object sender, EventArgs e)
        {
            if (HospitalListXAML.SelectedItem == null)
                return;

            HospitalModel hospital;

            hospital = HospitalListXAML.SelectedItem as HospitalModel;

            await Navigation.PushAsync(new ShowHospitalPage(hospital));
        }
    }
}