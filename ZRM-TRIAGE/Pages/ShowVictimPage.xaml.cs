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
    public partial class ShowVictimPage : ContentPage
    {
        private VictimModel _victimModel;
        public ShowVictimPage(VictimModel victimModel)
        {
            InitializeComponent();
            _victimModel = victimModel;

            VictimName.Text = _victimModel.Name;
            VictimSurname.Text = _victimModel.Surname;
            VictimCity.Text = _victimModel.City;
            VictimStreet.Text = _victimModel.Street;

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
            

        }
    }
}