using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Firebase.Database.Query;

namespace ZRM_TRIAGE
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EventCreatePage : ContentPage
    {
        public EventCreatePage()
        {
            InitializeComponent();
        }

        private async void PassCreateEventButtonClicked(object sender, EventArgs e)
        {

            EventCreateViewModel evM = new EventCreateViewModel();  

            if (!evM.CheckCorrectFormFields(EventName.Text, AmbulanceNumber.Text))
            {
                await DisplayAlert("Błąd", "Pole NAZWA ZDRAZENIA oraz NUMER ZESPOŁU nie mogą być puste", "OK");

                return;
            }

            if (evM.CheckEventExists(EventName.Text))
            {
                await DisplayAlert("Błąd", "Użyj innej nazwy zdarzenie - ta nazwa juz jest wykorzystywana", "OK");

                return;
            }

            evM.AddEventToDatabase(EventName.Text);

            AmbulanceModel ambulance = evM.AddMajorAmbulanceToDatabase(AmbulanceNumber.Text, EventName.Text);

            await DisplayAlert("TWÓJ KOD, ZAPISZ GO: ", ambulance.LoginCode, "OK");

            await Navigation.PushAsync(new KAMPage());


        }
    }
}