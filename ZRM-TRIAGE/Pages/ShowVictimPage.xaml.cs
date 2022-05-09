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
    public partial class ShowVictimPage : TabbedPage
    {
        private VictimModel _victimModel;
        private ShowVictimViewModel _showVictimViewModel;
        private List<AmbulanceModel> _ambulanceList;
        private List<HospitalModel> _hospitalList;
        public ShowVictimPage(VictimModel victimModel)
        {
            InitializeComponent();
            _showVictimViewModel = new ShowVictimViewModel();
            _ambulanceList = new List<AmbulanceModel>();
            _hospitalList = new List<HospitalModel>();
            _victimModel = victimModel;

            Title = _victimModel.Name + " " + _victimModel.Surname;

            GetLists();

            VictimName.Text = _victimModel.Name;
            VictimSurname.Text = _victimModel.Surname;
            VictimCity.Text = _victimModel.City;
            VictimStreet.Text = _victimModel.Street;
            VictimTriageColor.SelectedIndex = (int)_victimModel.Color;
            VictimBirth.Date = _victimModel.BirthDate;

            if (_victimModel.IsTransported)
                VictimStatus.Text = "Transportowany";
            else
                VictimStatus.Text = "Na miejscu";

            int index = -1;

            for (int i=0; i<_ambulanceList.Count; i++)
            {
                if (_ambulanceList[i].Number == _victimModel.Ambulance)
                {
                    index = i;
                    break;
                }
            }

            SelectAmbulance.SelectedIndex = index;

            for (int i = 0; i < _hospitalList.Count; i++)
            {
                if (_hospitalList[i].Name == _victimModel.Hospital)
                {
                    index = i;
                    break;
                }
            }

            SelectHospital.SelectedIndex = index;

            HeadInjury.IsChecked = _victimModel.Injuries.Head;
            NeckInjury.IsChecked = _victimModel.Injuries.Neck;
            NeckSpineInjury.IsChecked = _victimModel.Injuries.NeckSpine;
            ChestInjury.IsChecked = _victimModel.Injuries.Chest;
            StomachInjury.IsChecked= _victimModel.Injuries.Stomach;
            BackInjury.IsChecked = _victimModel.Injuries.Back;
            PelvisInjury.IsChecked = _victimModel.Injuries.Pelvis;
            LeftLegInjury.IsChecked = _victimModel.Injuries.LeftLeg;
            RightLegInjury.IsChecked = _victimModel.Injuries.RightLeg;
            LeftArmInjury.IsChecked = _victimModel.Injuries.LeftArm;
            RightArmInjury.IsChecked = _victimModel.Injuries.RightArm;
            BurnInjury.IsChecked = _victimModel.Injuries.Burn;
            FrostbiteInjury.IsChecked = _victimModel.Injuries.Frostbite;

            if (_victimModel.Injuries.Burn)
            {
                BurnPercent.Text = _victimModel.Injuries.PercentBurn.ToString();
            }
            if (_victimModel.Injuries.Frostbite)
            {
                PercentFrostbite.Text = _victimModel.Injuries.PercentFrostbite.ToString();
            }
        }

        private void GetLists()
        {
            _ambulanceList = _showVictimViewModel.GetTransportController().GetAmbulances();
            _hospitalList = _showVictimViewModel.GetTransportController().GetHospitals();

            for (int i = 0; i < _ambulanceList.Count; i++)
            {
                SelectAmbulance.Items.Add(_ambulanceList[i].Number);
            }

            for (int i=0; i<_hospitalList.Count;i++)
            {
                SelectHospital.Items.Add(_hospitalList[i].Name);
            }
        }

        private async void UpdateVictimButtonClicked(object sender, EventArgs e)
        {
            VictimModel victim = new VictimModel();

            ShowHospitalViewModel showHospitalViewModel = new ShowHospitalViewModel();

            bool choice = await DisplayAlert("Edycja poszkodowanego " + _victimModel.Name + " " + _victimModel.Surname, " Dokonać edycji?", "Tak", "nie");

            if (!choice)
                return;

            if (BurnInjury.IsChecked)
            {
                try
                {

                    if (BurnPercent.Text == null || BurnPercent.Text == "" || Int16.Parse(BurnPercent.Text) > 100 || Int16.Parse(BurnPercent.Text) < 0)
                    {
                        await DisplayAlert("Błąd edycji poszkodowanego!", "Wartość rozległości oparzeń powinna być w zakresie 0 - 100", "OK");
                        return;
                    }
                    victim.Injuries.PercentBurn = Int16.Parse(BurnPercent.Text);
                }catch 
                {
                    await DisplayAlert("Błąd edycji poszkodowanego!", "Wartość rozległości opażeń powinna być liczbą", "OK");
                    return;
                }
            }
            if (FrostbiteInjury.IsChecked)
            {
                try
                {
                    if (PercentFrostbite.Text == null || PercentFrostbite.Text == "" || Int16.Parse(PercentFrostbite.Text) > 100 || Int16.Parse(PercentFrostbite.Text) < 0)
                {
                    await DisplayAlert("Błąd edycji poszkodowanego!", "Wartość rozległości odmrożeń powinna być w zakresie 0 - 100", "OK");
                    return;
                }
                }
                catch 
                {
                    await DisplayAlert("Błąd edycji poszkodowanego!", "Wartość rozległości odmrożeń powinna być liczbą", "OK");
                    return;
                }
                victim.Injuries.PercentFrostbite = Int16.Parse(PercentFrostbite.Text);
            }

            victim.Id = _victimModel.Id;
            victim.Name = VictimName.Text;
            victim.Surname = VictimSurname.Text;
            victim.City = VictimCity.Text;
            victim.Street = VictimStreet.Text;
            victim.Color = (VictimModel.TriageColor)VictimTriageColor.SelectedIndex;
            victim.BirthDate = VictimBirth.Date;
            victim.IsTransported = _victimModel.IsTransported;
            victim.Hospital = _victimModel.Hospital;
            victim.HospitalId = _victimModel.HospitalId;
            victim.Ambulance = _victimModel.Ambulance;
            victim.AmbulanceId = _victimModel.AmbulanceId;

            victim.Injuries.Head = HeadInjury.IsChecked;
            victim.Injuries.Neck = NeckInjury.IsChecked;
            victim.Injuries.NeckSpine = NeckSpineInjury.IsChecked;
            victim.Injuries.Chest = ChestInjury.IsChecked;
            victim.Injuries.Stomach = StomachInjury.IsChecked;
            victim.Injuries.Back = BackInjury.IsChecked;
            victim.Injuries.Pelvis = PelvisInjury.IsChecked;
            victim.Injuries.LeftLeg = LeftLegInjury.IsChecked;
            victim.Injuries.RightLeg = RightLegInjury.IsChecked;
            victim.Injuries.LeftArm = LeftArmInjury.IsChecked;
            victim.Injuries.RightArm = RightArmInjury.IsChecked;
            victim.Injuries.Burn = BurnInjury.IsChecked;


            _showVictimViewModel.UpdateVictim(_victimModel, victim);

            await App.Current.MainPage.Navigation.PopAsync();
        }

        private async void DeleteVictimButtonClicked(object sender, EventArgs e)
        {
            string optionChoice = await DisplayPromptAsync("Uwaga!", "Na pewno chcesz usunąć poszkodowanego " + _victimModel.Name + " "+ _victimModel.Surname+ "? (wpisz: TAK jeśli chcesz usunąć)", "USUŃ", "ANULUJ", null, 3);

            if (optionChoice != null)
            {
                optionChoice = optionChoice.ToLower();

                if (optionChoice == "tak")
                {
                    _showVictimViewModel.DeleteVictim(_victimModel);

                    await App.Current.MainPage.Navigation.PopAsync();
                }
            }
        }

        private async void SaveTransportButtonClicked(object sender, EventArgs e)
        {
            if (SelectAmbulance.SelectedIndex == -1)
            {
                await DisplayAlert("Błąd transportu", "Wybierz karetkę!", "OK");
                return;
            }

            if (SelectHospital.SelectedIndex == -1)
            {
                await DisplayAlert("Błąd transportu", "Wybierz szpital docelowy!", "OK");
                return;
            }

            AmbulanceModel ambulance = new AmbulanceModel();
            HospitalModel hospital = new HospitalModel();

            _showVictimViewModel.GetTransportController().CorrectData(_victimModel);

            ambulance = _ambulanceList[SelectAmbulance.SelectedIndex];
            hospital = _hospitalList[SelectHospital.SelectedIndex];

            _victimModel.Ambulance = ambulance.Number;
            _victimModel.AmbulanceId = ambulance.Id;
            _victimModel.Hospital = hospital.Name;
            _victimModel.HospitalId = hospital.Id;

            ambulance.Victims.Add(_victimModel);
            hospital.VictimList.Add(_victimModel);

            _showVictimViewModel.GetTransportController().SaveData(_victimModel, ambulance, hospital);

            await App.Current.MainPage.Navigation.PopAsync();
        }
    }
}