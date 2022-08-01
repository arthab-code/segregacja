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
    public partial class AddHospitalPage : ContentPage
    {
        public AddHospitalPage()
        {
            InitializeComponent();
        }

        private async void PassAddHospitalButtonClicked(object sender, EventArgs e)
        {
            try
            {
                HospitalBuilderModel hospitalBuilder = new HospitalBuilderModel();

                HospitalModel hospital = new HospitalModel();

                hospital = hospitalBuilder.HospitalNameSet(HospitalName.Text)
               .HospitalCitySet(City.Text)
               .HospitalStreetSet(Street.Text)
               .Build();

                if (!hospitalBuilder.IsCreate)
                {
                    await DisplayAlert("Błąd dodawania szpitala!", hospitalBuilder.CreateText, "OK");
                    return;
                }

                HospitalAddViewModel hospitalAddViewModel = new HospitalAddViewModel();
                hospitalAddViewModel.AddHospital(hospital);

                await App.Current.MainPage.Navigation.PopAsync();
            }catch
            {
                await Navigation.PushAsync(new DatabaseErrorPage("SZPITAL"));
            }

        }
    }
}