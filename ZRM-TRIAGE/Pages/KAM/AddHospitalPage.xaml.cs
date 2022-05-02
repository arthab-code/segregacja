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
    public partial class AddHospitalPage : ContentPage
    {
        public AddHospitalPage()
        {
            InitializeComponent();
        }

        private async void PassAddAmbulanceButtonClicked(object sender, EventArgs e)
        {
            HospitalModel hospital = new HospitalModel();

            hospital.Name = HospitalName.Text;
            hospital.City = City.Text;
            hospital.Street = Street.Text;

            HospitalAddViewModel hospitalAddViewModel = new HospitalAddViewModel();
            hospitalAddViewModel.AddHospital(hospital);

            await App.Current.MainPage.Navigation.PopAsync();

        }
    }
}