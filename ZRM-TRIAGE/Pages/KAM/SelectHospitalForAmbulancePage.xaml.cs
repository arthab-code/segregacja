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
    public partial class SelectHospitalForAmbulancePage : ContentPage
    {
        private AmbulanceModel _ambulanceModel;
        private SelectHospitalForAmbulanceViewModel _selectHospitalForAmbulanceViewModel;
        public SelectHospitalForAmbulancePage(AmbulanceModel ambulance)
        {
            _ambulanceModel = ambulance;
            _selectHospitalForAmbulanceViewModel = new SelectHospitalForAmbulanceViewModel();

            Title = "Szpital docelowy karetki: " + _ambulanceModel.Number;

            InitializeComponent();

            TransportController transportController = new TransportController();

            HospitalListXAML.ItemsSource = transportController.GetHospitals();
        }

        private async void SelectButtonClicked(object sender, EventArgs e)
        {
            if (HospitalListXAML.SelectedItem == null)
                return;

            HospitalModel hospitalSelected = HospitalListXAML.SelectedItem as HospitalModel;

            _ambulanceModel.SelectedHospital = hospitalSelected;
            _ambulanceModel.HospitalId = hospitalSelected.DatabaseId;

            _selectHospitalForAmbulanceViewModel.SaveHospital(_ambulanceModel);

            await App.Current.MainPage.Navigation.PopAsync();

        }
    }
}