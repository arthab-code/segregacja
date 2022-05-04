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
    public partial class ShowHospitalPage : ContentPage
    {
        private HospitalModel _hospitalModel;
        public ShowHospitalPage(HospitalModel hospitalModel)
        {
            InitializeComponent();
            _hospitalModel = hospitalModel;

            HospitalName.Text = hospitalModel.Name;
            HospitalCity.Text = hospitalModel.City;
            HospitalStreet.Text = hospitalModel.Street;

            BindingContext = hospitalModel.VictimList;
            hospitalModel.VictimList.Add(new VictimModel { Name = "ZJEBUCH", Surname = "Zjebany" });
            hospitalModel.VictimList.Add(new VictimModel { Name = "ZJEBUCH1", Surname = "Zjebany2" });
            hospitalModel.VictimList.Add(new VictimModel { Name = "ZJEBUCH2", Surname = "Zjebany2" });
            VictimsListXAML.ItemsSource = hospitalModel.VictimList;
        }

        private async void DeleteHospitalButtonClicked(object sender, EventArgs e)
        {
            string optionChoice = await DisplayPromptAsync("Uwaga!", "Na pewno chcesz usunąć szpital " + _hospitalModel.Name + "? (wpisz: TAK jeśli chcesz usunąć)", "USUŃ", "ANULUJ", null, 3);

            ShowHospitalViewModel showHospitalViewModel = new ShowHospitalViewModel();

            if (optionChoice != null)
            {
                optionChoice = optionChoice.ToLower();

                if (optionChoice == "tak")
                {
                    showHospitalViewModel.DeleteHospital(_hospitalModel);

                    await App.Current.MainPage.Navigation.PopAsync();
                }
            }
        }

        private async void EditHospitalButtonClicked(object sender, EventArgs e)
        {
            HospitalModel hospital = new HospitalModel();

            ShowHospitalViewModel showHospitalViewModel = new ShowHospitalViewModel();

            bool choice = await DisplayAlert("Edycja szpitala " + _hospitalModel.Name, " Dokonać edycji?", "Tak", "nie");

            if (choice)
            {

                if (_hospitalModel.Name != HospitalName.Text)
                {
                    if (showHospitalViewModel.CheckHospitalExists(HospitalName.Text))
                    {
                        await DisplayAlert("Błąd", "Takai szpital już jest dodany", "OK");
                        return;
                    }
                }

            }

            hospital.Name = HospitalName.Text;
            hospital.City = HospitalCity.Text;
            hospital.Street = HospitalStreet.Text;

            showHospitalViewModel.UpdateHospital(_hospitalModel, hospital);

            await App.Current.MainPage.Navigation.PopAsync();
        }
    }
}