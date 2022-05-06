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
    public partial class KAMVictimsPage : TabbedPage
    {
        public KAMVictimsPage()
        {
            InitializeComponent();

        }

        protected override void OnAppearing()
        {
            VictimRepository vr = new VictimRepository();

            var redVictimsList = vr.GetAll().Where(a => a.Color == VictimModel.TriageColor.Red).ToList<VictimModel>();
            var yellowVictimsList = vr.GetAll().Where(a => a.Color == VictimModel.TriageColor.Yellow).ToList<VictimModel>();
            var greenVictimsList = vr.GetAll().Where(a => a.Color == VictimModel.TriageColor.Green).ToList<VictimModel>();
            var blackVictimsList = vr.GetAll().Where(a => a.Color == VictimModel.TriageColor.Black).ToList<VictimModel>();

            //   BindingContext = redVictimsList;

            RedVictimsList.ItemsSource = redVictimsList;
            YellowVictimsList.ItemsSource = yellowVictimsList;
            GreenVictimsList.ItemsSource = greenVictimsList;
            BlackVictimsList.ItemsSource = blackVictimsList;
        }
        private async void AddVictimButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddVictimPage());
        }

        private async void RedVictimsListShowVictim(object sender, ItemTappedEventArgs e)
        {
            await Navigation.PushAsync(new ShowVictimPage((VictimModel)RedVictimsList.SelectedItem));

        }

        private async void YellowVictimsListShowVictim(object sender, ItemTappedEventArgs e)
        {
            await Navigation.PushAsync(new ShowVictimPage((VictimModel)YellowVictimsList.SelectedItem));
        }

        private async void GreenVictimsListShowVictim(object sender, ItemTappedEventArgs e)
        {
            await Navigation.PushAsync(new ShowVictimPage((VictimModel)GreenVictimsList.SelectedItem));
        }

        private async void BlackVictimsListShowVictim(object sender, ItemTappedEventArgs e)
        {
            await Navigation.PushAsync(new ShowVictimPage((VictimModel)BlackVictimsList.SelectedItem));
        }
    }
}