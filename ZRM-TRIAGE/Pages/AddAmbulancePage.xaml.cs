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
public partial class AddAmbulancePage : ContentPage
{
        private AmbulanceViewModel _ambulanceVM;
        public AddAmbulancePage(AmbulanceViewModel ambulanceVM)
        {
            InitializeComponent();

            _ambulanceVM = ambulanceVM;
        }

        private void PassAddAmbulanceButtonClicked(object sender, EventArgs e)
        {
            AmbulanceBuilderModel ambulanceBuilder = new AmbulanceBuilderModel();

            AmbulanceModel ambulance = ambulanceBuilder
                                       .AmbulanceSetNumber(AmbulanceNumber.Text)
                                       .AmbulanceFunctionSet(AmbulanceFunction.Text)
                                       .LoginCodeGenerate()
                                       .AmbulanceStatusSet()
                                       .AmbulanceHospitalSet()
                                       .AmbulanceVictimSet()
                                       .Build();

            _ambulanceVM.AmbulanceList.Add(ambulance);

            DisplayAlert("Dodano zespół " + ambulance.Number, "Przekaż kod logowania: " + ambulance.LoginCode, "OK");

            App.Current.MainPage.Navigation.PopAsync();

        }
    }
}