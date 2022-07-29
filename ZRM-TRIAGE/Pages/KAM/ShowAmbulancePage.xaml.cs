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
    public partial class ShowAmbulancePage : TabbedPage
    {

        private AmbulanceModel _ambulance;
        ShowAmbulanceViewModel _showAmbulanceVM;

        public ShowAmbulancePage(AmbulanceModel ambulance)
        {          
            InitializeComponent();
            _ambulance = ambulance;
        }

        private void TargetHospitalUpdate()
        {
            if (_ambulance.SelectedHospital == null)
            {
                TargetHospital.Text = "NIE WYBRANO";
                CancelHospital.IsVisible = false;
                SelectHospital.IsVisible = true;
            }
            else
            {
                TargetHospital.Text = _ambulance.SelectedHospital.Name;
                CancelHospital.IsVisible = true;
                SelectHospital.IsVisible = false;
            }
        }

        protected override void OnAppearing()
        {
            _showAmbulanceVM = new ShowAmbulanceViewModel();

            AmbulanceModel.Function function = _showAmbulanceVM.AmbulanceFunctionAdd((int)_ambulance.AmbulanceFunction);

            TargetHospitalUpdate();

            AmbulanceFunction.SelectedIndex = (int)_ambulance.AmbulanceFunction;
            AmbulanceStatus.SelectedIndex = (int)_ambulance.AmbulanceStatus;
            AmbulanceNumber.Text = _ambulance.Number;
            AmbulanceLoginCode.Text = _ambulance.LoginCode;

            RefreshVictimList();
        }

        private async void DeleteAmbulanceClicked(object sender, EventArgs e)
        {
            if (_ambulance.AmbulanceFunction == AmbulanceModel.Function.Major)
            {
                await DisplayAlert("Błąd", "Nie możesz usunąć swojego zespołu", "OK");
                return;
            }


            AmbulanceRepository ambulanceRepos = new AmbulanceRepository();

            string optionChoice = await DisplayPromptAsync("Uwaga!", "Na pewno chcesz usunąć karetkę " + _ambulance.Number + "? (wpisz: TAK jeśli chcesz usunąć)", "USUŃ", "ANULUJ",null,3);

            if (optionChoice != null)
            {
                optionChoice = optionChoice.ToLower();

                if (optionChoice == "tak")
                {
                    ambulanceRepos.Remove(_ambulance.DatabaseId);

                    await App.Current.MainPage.Navigation.PopAsync();
                }
            }
        }

        private async void UpdateButtonClicked(object sender, EventArgs e)
        {
            bool choice = await DisplayAlert("Edycja " + _ambulance.Number, "Dokonać edycji?", "Tak", "Nie");

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

                if (_ambulance.AmbulanceFunction != (AmbulanceModel.Function)AmbulanceFunction.SelectedIndex)
                {
                    newAmbulance.AmbulanceFunction = (AmbulanceModel.Function)AmbulanceFunction.SelectedIndex;
                    if (_showAmbulanceVM.CheckAmbulanceFunctionExists(newAmbulance.AmbulanceFunction))
                    {
                        await DisplayAlert("Błąd", "Taka funkcja jest już przypisana", "OK");
                        return;
                    }

                }

                _showAmbulanceVM.Update(newAmbulance);
                await App.Current.MainPage.Navigation.PopAsync();
            }
        }

        private async void VictimsListXAMLItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (VictimListXAML.SelectedItem == null)
                return;

            VictimModel victim = new VictimModel();

            victim = VictimListXAML.SelectedItem as VictimModel;

            await Navigation.PushAsync(new ShowVictimPage(victim));
        }

        private async void SelectHospitalButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SelectHospitalForAmbulancePage(_ambulance));
        }

        private void SelectVictimButtonClicked(object sender, EventArgs e)
        {
             Navigation.PushAsync(new SelectVictimsForAmbulancePage(_ambulance));
            RefreshVictimList();
        }

        private void DeleteVictimButtonClicked(object sender, EventArgs e)
        {
            if (VictimListXAML.SelectedItem == null)
                return;

            VictimModel victimModel = VictimListXAML.SelectedItem as VictimModel;

            victimModel.AmbulanceId = null;
            victimModel.Ambulance = null;
            victimModel.HospitalId = null;
            victimModel.Hospital = null;

            VictimRepository victimRepository = new VictimRepository();

            victimRepository.Update(victimModel);

            DisplayAlert("Usunięto", victimModel.Name + " " + victimModel.Surname, "OK");

            RefreshVictimList();

        }

        private void RefreshVictimList()
        {
            VictimListXAML.BeginRefresh();
            var victimList = _showAmbulanceVM.GetTransportController().GetVictims().Where<VictimModel>(a => a.AmbulanceId == _ambulance.DatabaseId).ToList();
            BindingContext = victimList;          
            VictimListXAML.ItemsSource = victimList;
            VictimListXAML.EndRefresh();
        }

        private void CancelHospitalButtonClicked(object sender, EventArgs e)
        {
            _ambulance.HospitalId = null;
            _ambulance.HospitalName = null;
            _ambulance.SelectedHospital = null;

            TargetHospitalUpdate();
        }
    }
}