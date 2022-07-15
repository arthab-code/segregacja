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
                InfoText.Text = "CZERWONA STREFA";

            if (ambulance.AmbulanceFunction == AmbulanceModel.Function.Yellow)
                InfoText.Text = "ŻÓŁTA STREFA";

            if (ambulance.AmbulanceFunction == AmbulanceModel.Function.Green)
                InfoText.Text = "ZIELONA STREFA";



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
            FileSystem fs = new FileSystem();
            fs.DeleteFile();

            UserInfo.SetEventId(null);
            UserInfo.SetAmbulanceNumber(null);
            UserInfo.SetAmbulance(null);

            await Navigation.PushAsync(new MainPage());
        }

    }
}