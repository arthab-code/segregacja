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

        }
    }
}