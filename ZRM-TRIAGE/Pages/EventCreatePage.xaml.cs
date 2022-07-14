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
            try
            {
                EventCreateViewModel evM = new EventCreateViewModel();  

            if (!evM.CheckCorrectFormFields(AmbulanceNumber.Text))
            {
                await DisplayAlert("Błąd", "Uzupełnij pole Numer Zespołu", "OK");

                return;
            }

        /*    if (evM.CheckEventExists(EventName.Text))
            {
                await DisplayAlert("Błąd", "Użyj innej nazwy zdarzenie - ta nazwa juz jest wykorzystywana", "OK");

                return;
            } */

                evM.AddEventToDatabase();
                string eventNumber = evM.GetEventNumber();
                AmbulanceModel ambulance = evM.AddMajorAmbulanceToDatabase(AmbulanceNumber.Text, eventNumber);

                evM.CreateTriageDB();

                await DisplayAlert("TWÓJ KOD, ZAPISZ GO: ", ambulance.LoginCode, "OK");

                FileSystem fs = new FileSystem();
                fs.CreateDataFile(ambulance.LoginCode, UserInfo.EventId);

                await Navigation.PushAsync(new KAMPage());

            } catch (Exception ex)
            {
                await DisplayAlert("Connection Database Error","Nie można połączyć się z bazą danych, skontaktuj się z administratorem\n"+ex.Message, "OK");
                return;
            }


        }
    }
}