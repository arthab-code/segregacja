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
    public partial class AddVictimPage : ContentPage
    {
      
        public AddVictimPage()
        {
            InitializeComponent();
        }

        private async void PassAddVictimButtonClicked(object sender, EventArgs e)
        {

            VictimModel victim = new VictimModel();
            VictimBuilderModel victimBuilder = new VictimBuilderModel();
            victim =  victimBuilder
                     .SetVictimName(VictimName.Text)
                     .SetVictimSurname(VictimSurname.Text)
                     .SetVictimCity(VictimCity.Text)
                     .SetVictimStreet(VictimStreet.Text)
                     .SetVictimTriageColor(VictimTriageColor.SelectedIndex)
                     .SetVictimBirthDate(VictimBirth.Date)
                     .Build();

            InjuriesCreatorModel injuriesCreatorModel = new InjuriesCreatorModel(victim.Injuries);

            injuriesCreatorModel.Head(HeadInjury.IsChecked);
            injuriesCreatorModel.Neck(NeckInjury.IsChecked);
            injuriesCreatorModel.SpineNeck(NeckSpineInjury.IsChecked);
            injuriesCreatorModel.Chest(ChestInjury.IsChecked);
            injuriesCreatorModel.Stomach(StomachInjury.IsChecked);
            injuriesCreatorModel.Back(BackInjury.IsChecked);
            injuriesCreatorModel.Pelvis(PelvisInjury.IsChecked);
            injuriesCreatorModel.LeftLeg(LeftLegInjury.IsChecked);
            injuriesCreatorModel.RightLeg(RightLegInjury.IsChecked);
            injuriesCreatorModel.LeftArm(LeftArmInjury.IsChecked);
            injuriesCreatorModel.RightArm(RightArmInjury.IsChecked);
         //   injuriesCreatorModel.Burn(BurnInjury.IsChecked, Int16.Parse(BurnPercent.Text));
         //   injuriesCreatorModel.Frostbite(FrostbiteInjury.IsChecked, Int16.Parse(PercentFrostbite.Text));
            
            if (!victimBuilder.IsCreated)
            {
                await DisplayAlert("Błąd dodawania poszkodowanego!", victimBuilder.CreateText, "OK");
                return;
            }

            AddVictimViewModel addVictimViewModel = new AddVictimViewModel();

            addVictimViewModel.AddVictim(victim);

            await App.Current.MainPage.Navigation.PopAsync();

        }
    }
}