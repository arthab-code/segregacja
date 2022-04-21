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
    }
}