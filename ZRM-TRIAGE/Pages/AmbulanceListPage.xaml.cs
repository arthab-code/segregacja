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
public partial class AmbulanceListPage : ContentPage
{
    private AmbulanceViewModel ambulanceVM;
    public AmbulanceListPage()
    {
        InitializeComponent();

            ambulanceVM = new AmbulanceViewModel();
            
          //  BindingContext = ambulanceVM.AmbulanceList;
            

          //  AmbulanceListXAML.ItemsSource = ambulanceVM.AmbulanceList;
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
    }
}