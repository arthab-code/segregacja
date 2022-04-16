using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ZRM_TRIAGE
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class AmbulanceListPage : ContentPage
{
    private AmbulanceListViewModel ambulanceVM;
    public AmbulanceListPage()
    {
        InitializeComponent();

            ambulanceVM = new AmbulanceListViewModel();
    }

        protected override async void OnAppearing()
        {
            AmbulanceListViewModel aLvM = new AmbulanceListViewModel();

            var list = await aLvM.GetAllAmbulances();
            BindingContext = list;
            AmbulanceListXAML.ItemsSource = list;
        }

        private async void AddAmbulanceButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddAmbulancePage(ambulanceVM));
        }
        private async void ShowAmbulanceButtonClicked(object sender, EventArgs e)
        {
            if (AmbulanceListXAML.SelectedItem == null)
                return;

            AmbulanceModel ambulance;

            ambulance = AmbulanceListXAML.SelectedItem as AmbulanceModel;

            await Navigation.PushAsync(new ShowAmbulancePage(ambulance));
        }

        
    }
}