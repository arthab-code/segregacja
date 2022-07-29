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

        public ShowVictimPage(VictimModel victimModel)
        {
            InitializeComponent();

            _victimModel = victimModel;
            _showVictimViewModel = new ShowVictimViewModel();

            Title = _victimModel.Name + " " + _victimModel.Surname;

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


            HospitalTransport.Text = _victimModel.Hospital;
            AmbulanceTransport.Text = _victimModel.Ambulance;



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

            VictimStatusChange();

        }

        private void VictimStatusChange()
        {
            AmbulanceRepository ambulanceRepository = new AmbulanceRepository();

            AmbulanceModel ambulance = ambulanceRepository.GetAmbulance(_victimModel.AmbulanceId);

            if (ambulance == null) return;

            if (ambulance.AmbulanceStatus == AmbulanceModel.Status.AtLocation)
                VictimStatus.Text = "NA MIEJSCU";
            if (ambulance.AmbulanceStatus == AmbulanceModel.Status.Transport)
                VictimStatus.Text = "TRANSPORT DO SZPITALA";
            if (ambulance.AmbulanceStatus == AmbulanceModel.Status.Hospital)
                VictimStatus.Text = "W SZPITALU";
        }

        private async void UpdateVictimButtonClicked(object sender, EventArgs e)
        {
            VictimModel victim = new VictimBuilderModel()
                .SetVictimName(VictimName.Text)
                .SetVictimSurname(VictimSurname.Text)
                .SetVictimCity(VictimCity.Text)
                .SetVictimStreet(VictimStreet.Text)
                .SetVictimTriageColor(VictimTriageColor.SelectedIndex)
                .SetVictimBirthDate(VictimBirth.Date)
                .SetIsTransported(_victimModel.IsTransported)
                .Build();

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

            victim.DatabaseId = _victimModel.DatabaseId;
           /* victim.Name = VictimName.Text;
            victim.Surname = VictimSurname.Text;
            victim.City = VictimCity.Text;
            victim.Street = VictimStreet.Text;
            victim.Color = (VictimModel.TriageColor)VictimTriageColor.SelectedIndex;
            victim.BirthDate = VictimBirth.Date;
            victim.IsTransported = _victimModel.IsTransported; */
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

            _showVictimViewModel.UpdateVictim(victim);

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

      
    }
}