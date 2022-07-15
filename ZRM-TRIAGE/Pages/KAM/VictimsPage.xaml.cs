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
            KAMVictimsViewModel _KAMVictimsViewModel = new KAMVictimsViewModel();

            var redVictimsList = _KAMVictimsViewModel.GetVictimList(VictimModel.TriageColor.Red);
            var yellowVictimsList = _KAMVictimsViewModel.GetVictimList(VictimModel.TriageColor.Yellow);
            var greenVictimsList = _KAMVictimsViewModel.GetVictimList(VictimModel.TriageColor.Green);
            var blackVictimsList = _KAMVictimsViewModel.GetVictimList(VictimModel.TriageColor.Black);

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