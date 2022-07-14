using Firebase.Database.Query;
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
        private AmbulanceListViewModel _ambulanceVM;
        public AddAmbulancePage(AmbulanceListViewModel ambulanceVM)
        {
            InitializeComponent();

            _ambulanceVM = ambulanceVM;
        }

        private async void PassAddAmbulanceButtonClicked(object sender, EventArgs e)
        {

            AmbulanceBuilderModel ambulanceBuilder = new AmbulanceBuilderModel();
            AddAmbulanceViewModel addAmbulanceViewModel = new AddAmbulanceViewModel();

            if (addAmbulanceViewModel.CheckAmbulanceExists(AmbulanceNumber.Text))
            {
                await DisplayAlert("Błąd", "Zespół " + AmbulanceNumber.Text + " już bierze udział w zdarzeniu ","OK");
                return;
            }

            AmbulanceModel.Function ambulanceFunction = addAmbulanceViewModel.AmbulanceFunctionAdd(AmbulanceFunction.SelectedIndex);

            if(addAmbulanceViewModel.CheckAmbulanceFunctionExists(ambulanceFunction))
            {
                await DisplayAlert("Bład", "Ta funkcja zostałą już przydzielona innemu zespołowi", "OK");
                return;
            }

            AmbulanceModel ambulance = ambulanceBuilder
                                       .AmbulanceSetNumber(AmbulanceNumber.Text)
                                       .AmbulanceFunctionSet(ambulanceFunction)
                                       //.AmbulanceSetEventId(UserInfo.EventId)
                                       .LoginCodeGenerate()
                                       .AmbulanceStatusSet()
                                       .AmbulanceHospitalSet()
                                       .Build();

            addAmbulanceViewModel.AddAmbulanceToDatabase(ambulance);

            await DisplayAlert("Dodano zespół " + ambulance.Number, "Przekaż kod logowania: " + ambulance.LoginCode, "OK");

            await App.Current.MainPage.Navigation.PopAsync();

        }
    }
}