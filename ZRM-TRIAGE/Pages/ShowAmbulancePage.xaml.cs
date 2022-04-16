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
        public ShowAmbulancePage(AmbulanceModel ambulance)
        {          
            InitializeComponent();

            AddAmbulanceViewModel aAvM = new AddAmbulanceViewModel();

            _ambulance = ambulance;
            int i = (int)_ambulance.AmbulanceFunction;
            AmbulanceModel.Function function = aAvM.AmbulanceFunctionAdd((int)_ambulance.AmbulanceFunction);

            AmbulanceFunction.SelectedIndex = (int)_ambulance.AmbulanceFunction;
            AmbulanceNumber.Text = _ambulance.Number;
        }
    }
}