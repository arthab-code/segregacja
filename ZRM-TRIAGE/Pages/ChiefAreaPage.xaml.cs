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
    public partial class ChiefAreaPage : ContentPage
    {
        public ChiefAreaPage(AmbulanceModel ambulance)
        {
            InitializeComponent();

            if (ambulance.AmbulanceFunction == AmbulanceModel.Function.Red)
                Subtitle.Text = "CZERWONA STREFA DLA ZDARZENIA NR "+UserInfo.EventId;

            if (ambulance.AmbulanceFunction == AmbulanceModel.Function.Yellow)
                Subtitle.Text = "ŻÓŁTA STREFA DLA ZDARZENIA NR" + UserInfo.EventId;

            if (ambulance.AmbulanceFunction == AmbulanceModel.Function.Green)
                Subtitle.Text = "ZIELONA STREFA DLA ZDARZENIA NR" + UserInfo.EventId;

        }

        private async void AmbulanceClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AmbulanceListPage());
        }


        private async void VictimsButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new VictimsPage());
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

    }
}