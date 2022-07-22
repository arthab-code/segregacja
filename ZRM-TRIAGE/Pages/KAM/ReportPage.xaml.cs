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
    public partial class ReportPage : ContentPage
    {
        public ReportPage()
        {
            InitializeComponent();
        }

        private void ReportGenerateButtonClicked(object sender, EventArgs e)
        {
            ReportGenerator reportGenerator = new ReportGenerator(EventPlace.Text, UserInfo.Ambulance.ChiefPersonalData);
            reportGenerator.LoadData();
            reportGenerator.Generate();
        }
    }
}