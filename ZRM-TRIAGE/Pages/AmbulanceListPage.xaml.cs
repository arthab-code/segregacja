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
            
            ambulanceVM.AmbulanceList.Add(new AmbulanceModel
            {
                Number = "S02450",
                LoginCode = "12345",
                AmbulanceStatus = AmbulanceModel.Status.AtLocation
            });

            ambulanceVM.AmbulanceList.Add(new AmbulanceModel
            {
                Number = "S02476",
                LoginCode = "12345",
                AmbulanceStatus = AmbulanceModel.Status.AtLocation
            });

            ambulanceVM.AmbulanceList.Add(new AmbulanceModel
            {
                Number = "S02476",
                LoginCode = "12345",
                AmbulanceStatus = AmbulanceModel.Status.AtLocation
            });

            BindingContext = ambulanceVM.AmbulanceList;
            AmbulanceListXAML.ItemsSource = ambulanceVM.AmbulanceList;

     }

        private async void AddAmbulanceButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddAmbulancePage(ambulanceVM));
        }
    }
}