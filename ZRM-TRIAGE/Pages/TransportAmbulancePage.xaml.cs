using Firebase.Database.Query;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ZRM_TRIAGE
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TransportAmbulancePage : ContentPage
    {

        private TransportAmbulanceViewModel _tAVM;
        public TransportAmbulancePage()
        {
            InitializeComponent();
            _tAVM = new TransportAmbulanceViewModel();
            Subtitle.Text = "ZESPÓŁ TRANSPORTOWY "+UserInfo.Ambulance.Number+" DLA ZDARZENIA NR "+UserInfo.EventId;
        }

        protected override void OnAppearing()
        {
            if (UserInfo.Ambulance.AmbulanceStatus == AmbulanceModel.Status.AtLocation)
                ChangeColor(AtLocation, Transport, Hospital);

            if (UserInfo.Ambulance.AmbulanceStatus == AmbulanceModel.Status.Transport)
                ChangeColor(Transport, AtLocation, Hospital);

            if (UserInfo.Ambulance.AmbulanceStatus == AmbulanceModel.Status.Hospital)
                ChangeColor(Hospital, Transport, AtLocation);

             VictimsList.ItemsSource = _tAVM.GetVictims();
             HospitalList.ItemsSource = _tAVM.GetHospital();


        }

        private void ChangeColor(Button toGreen, Button toStandardOne, Button toStandardTwo)
        {
            toGreen.BackgroundColor = Color.FromHex("#ACD98A");
            toGreen.BorderColor = Color.FromHex("#ACD98A");

            toStandardOne.BackgroundColor = Color.FromHex("#F0F7F0");
            toStandardOne.BorderColor = Color.Black;

            toStandardTwo.BackgroundColor = Color.FromHex("#F0F7F0");
            toStandardTwo.BorderColor = Color.Black;
        }

        private void AtLocation_Clicked(object sender, EventArgs e)
        {
            ChangeColor(AtLocation, Transport, Hospital);
            _tAVM.ChangeAmbulanceStatus(UserInfo.Ambulance, AmbulanceModel.Status.AtLocation);
        }

        private void Transport_Clicked(object sender, EventArgs e)
        {
            ChangeColor(Transport, AtLocation, Hospital);
            _tAVM.ChangeAmbulanceStatus(UserInfo.Ambulance, AmbulanceModel.Status.Transport);
        }

        private void Hospital_Clicked(object sender, EventArgs e)
        {
            ChangeColor(Hospital,Transport, AtLocation);
            _tAVM.ChangeAmbulanceStatus(UserInfo.Ambulance, AmbulanceModel.Status.Hospital);
        }

        private async void ExitButtonClicked(object sender, EventArgs e)
        {
            string optionChoice = await DisplayPromptAsync("Uwaga!", "Na pewno chcesz opuscic zdarzenie? (wpisz: TAK jeśli chcesz usunąć)", "OPUŚĆ", "ANULUJ", null, 3);

            if (optionChoice != null)
            {
                optionChoice = optionChoice.ToLower();

                if (optionChoice != "tak")
                {
                    return;
                }
            }
            FileSystem fs = new FileSystem();
            fs.DeleteFile();

            UserInfo.SetEventId(null);
            UserInfo.SetAmbulanceNumber(null);
            UserInfo.SetAmbulance(null);

            await Navigation.PushAsync(new MainPage());
        }

        private async void VictimsListShowVictim(object sender, ItemTappedEventArgs e)
        {
            await Navigation.PushAsync(new ShowVictimPage((VictimModel)VictimsList.SelectedItem));
        }
    }
}