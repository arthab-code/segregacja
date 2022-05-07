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

            if (!victimBuilder.IsCreated)
            {
                await DisplayAlert("Błąd dodawania poszkodowanego!", victimBuilder.CreateText, "OK");
                return;
            }

            InjuriesCreatorModel injuriesCreatorModel = new InjuriesCreatorModel(victim.Injuries);

            if (BurnInjury.IsChecked)
            {
                try
                {

                    if (BurnPercent.Text == null || BurnPercent.Text == " ")
                    {
                        await DisplayAlert("Błąd dodawania poszkodowanego!", "Określ rozległośc oparzeń", "OK");
                        return;
                    }

                    if (Int16.Parse(BurnPercent.Text) > 100 || Int16.Parse(BurnPercent.Text) < 0)
                    {
                        await DisplayAlert("Błąd dodawania poszkodowanego!", "Wartość rozległości oparzeń powinna być w zakresie 0 - 100", "OK");
                        return;
                    }
                }
                catch
                {
                    await DisplayAlert("Błąd edycji poszkodowanego!", "Wartość rozległości opażeń powinna być liczbą", "OK");
                    return;
                }

                injuriesCreatorModel.Burn(BurnInjury.IsChecked, Int16.Parse(BurnPercent.Text));
            }

            if (FrostbiteInjury.IsChecked)
            {
                try
                {
                    if (PercentFrostbite.Text == null || PercentFrostbite.Text == " ")
                    {
                        await DisplayAlert("Błąd dodawania poszkodowanego!", "Określ rozległośc odmrożeń", "OK");
                        return;
                    }

                    if (Int16.Parse(PercentFrostbite.Text) > 100 || Int16.Parse(PercentFrostbite.Text) < 0)
                    {
                        await DisplayAlert("Błąd dodawania poszkodowanego!", "Wartość rozległości odmrożeń powinna być w zakresie 0 - 100", "OK");
                        return;
                    }

                }
                catch
                {
                    await DisplayAlert("Błąd edycji poszkodowanego!", "Wartość rozległości odmrożeń powinna być liczbą", "OK");
                    return;
                }

                injuriesCreatorModel.Frostbite(FrostbiteInjury.IsChecked, Int16.Parse(PercentFrostbite.Text));
            }

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

            AddVictimViewModel addVictimViewModel = new AddVictimViewModel();

            addVictimViewModel.AddVictim(victim);

            await App.Current.MainPage.Navigation.PopAsync();

        }
    }
}