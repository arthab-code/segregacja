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

            BindingContext = redVictimsList;

            RedVictimsList.ItemsSource = redVictimsList;
        }
        private async void AddVictimButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddVictimPage());
        }

        private async void RedVictimsListShowVictim(object sender, ItemTappedEventArgs e)
        {
            await Navigation.PushAsync(new ShowVictimPage((VictimModel)RedVictimsList.SelectedItem));

        }
    }
}