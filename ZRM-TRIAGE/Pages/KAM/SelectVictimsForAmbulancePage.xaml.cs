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
    public partial class SelectVictimsForAmbulancePage : ContentPage
    {
        private AmbulanceModel _ambulanceModel;
        private SelectVictimsForAmbulanceViewModel _selectVictimsForAmbulanceViewModel;
        public SelectVictimsForAmbulancePage(AmbulanceModel ambulance)
        {

            InitializeComponent();

            _selectVictimsForAmbulanceViewModel = new SelectVictimsForAmbulanceViewModel();

            _ambulanceModel = ambulance;
            Title = "Wybierz poszkodowanych dla karetki: " + _ambulanceModel.Number;

            TransportController transportController = new TransportController();

            VictimListXAML.ItemsSource = transportController.GetVictims().Where<VictimModel>(a => a.AmbulanceId == null);
            
        }

        private void SelectButtonClicked(object sender, EventArgs e)
        {
            if (VictimListXAML.SelectedItem == null)
                return;

            VictimModel victimSelected = VictimListXAML.SelectedItem as VictimModel;

            victimSelected.AmbulanceId = _ambulanceModel.DatabaseId;
            victimSelected.Ambulance = _ambulanceModel.Number;
            victimSelected.HospitalId = _ambulanceModel.HospitalId;
            var selectedHospital = _ambulanceModel.SelectedHospital;

            if (selectedHospital != null)
               victimSelected.Hospital = selectedHospital.Name;

            _selectVictimsForAmbulanceViewModel.AddVictim(victimSelected);
             App.Current.MainPage.Navigation.PopAsync();

            DisplayAlert("Dodano", victimSelected.Name + " " + victimSelected.Surname, "OK");
        }
    }
}