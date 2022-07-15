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
    public partial class MajorRedPage : ContentPage
    {
        public MajorRedPage()
        {
            InitializeComponent();
        }

        private async void AmbulanceClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AmbulanceListPage());
        }


        private async void VictimsButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new KAMVictimsPage());
        }

        private async void ExitButtonClicked(object sender, EventArgs e)
        {
            FileSystem fs = new FileSystem();
            fs.DeleteFile();

            UserInfo.SetEventId(null);
            UserInfo.SetAmbulanceNumber(null);

            await Navigation.PushAsync(new MainPage());
        }

    }
}