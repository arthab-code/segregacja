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
public partial class KAMPage : ContentPage
{
        private TriageModel _triageModel;
    public KAMPage()
    {
        InitializeComponent();

            Subtitle.Text = "PANEL KAM DLA ZDARZENIA NR " + UserInfo.EventId;
    }
        protected override void OnAppearing()
        {
            AmbulanceListViewModel aLvM = new AmbulanceListViewModel();

            TriageRepository triageRepos = new TriageRepository();
            triageRepos.GetTriageModel();
            _triageModel = triageRepos.GetTriageModelObject();

            RedAmount.Text = _triageModel.Red.ToString();
            YellowAmount.Text = _triageModel.Yellow.ToString();
            GreenAmount.Text = _triageModel.Green.ToString();
            BlackAmount.Text = _triageModel.Black.ToString();
        }

        protected override bool OnBackButtonPressed()
        {
            return true;
        }

        private async void ProceduresClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Procedures());
        }

        private async void AmbulanceClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AmbulanceListPage());
        }

        private async void TriageButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new TriagePage(_triageModel));
        }

        private async void VictimsButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new VictimsPage());
        }

        private async void HospitalsButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new HospitalsListPage());
        }

        private async void ReportButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ReportPage());
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