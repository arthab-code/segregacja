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
    public partial class ShowAmbulancePage : ContentPage
    {

        private AmbulanceModel _ambulance;
        ShowAmbulanceViewModel _showAmbulanceVM;

        public ShowAmbulancePage(AmbulanceModel ambulance)
        {          
            InitializeComponent();

            _showAmbulanceVM = new ShowAmbulanceViewModel();

            _ambulance = ambulance;
            AmbulanceModel.Function function = _showAmbulanceVM.AmbulanceFunctionAdd((int)_ambulance.AmbulanceFunction);

            AmbulanceFunction.SelectedIndex = (int)_ambulance.AmbulanceFunction;
            AmbulanceStatus.SelectedIndex = (int)_ambulance.AmbulanceStatus;
            AmbulanceNumber.Text = _ambulance.Number;
            AmbulanceLoginCode.Text = _ambulance.LoginCode;
        }

        private async void DeleteAmbulanceClicked(object sender, EventArgs e)
        {
            if (_ambulance.AmbulanceFunction == AmbulanceModel.Function.Major)
            {
                await DisplayAlert("Błąd", "Nie możesz usunąć swojego zespołu", "OK");
                return;
            }


            AmbulanceRepository ambulanceRepos = new AmbulanceRepository();

            string optionChoice = await DisplayPromptAsync("Uwaga!", "Na pewno chcesz usunąć karetkę " + _ambulance.Number + "? (wpisz: tak jeśli chcesz usunąć)", "USUŃ", "ANULUJ",null,3);

            if (optionChoice != null)
            {
                optionChoice = optionChoice.ToLower();

                if (optionChoice == "tak")
                {
                    ambulanceRepos.Remove(_ambulance.Number);

                    await App.Current.MainPage.Navigation.PopAsync();
                }
            }
        }

        private async void UpdateButtonClicked(object sender, EventArgs e)
        {
            bool choice = await DisplayAlert("Edycja " + _ambulance.Number, "Dokonać edycji?", "Tak", "nie");

            if (choice)
            {

                AmbulanceModel newAmbulance = new AmbulanceModel(_ambulance);
                newAmbulance.Number = AmbulanceNumber.Text;

                if (newAmbulance.Number != _ambulance.Number)
                {
                    if (_showAmbulanceVM.CheckAmbulanceExists(newAmbulance.Number))
                    {
                        await DisplayAlert("Błąd", "Taka karetka bierze już udział w zdarzeniu", "OK");
                        return;
                    }
                }
                newAmbulance.AmbulanceFunction = (AmbulanceModel.Function)AmbulanceFunction.SelectedIndex;
                if (_showAmbulanceVM.CheckAmbulanceFunctionExists(newAmbulance.AmbulanceFunction))
                {
                    await DisplayAlert("Błąd", "Taka funkcja jest już przypisana", "OK");
                    return;
                }

                _showAmbulanceVM.Update(_ambulance, newAmbulance);
                await App.Current.MainPage.Navigation.PopAsync();
            }
        }
    }
}