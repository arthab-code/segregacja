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

            if (!evM.CheckChiefPersonalData(ChiefName.Text, ChiefSurname.Text))
            {
                    await DisplayAlert("Błąd", "Uzupełnij imię i nazwisko kierownika zespołu", "OK");

                    return;
            }

        /*    if (evM.CheckEventExists(EventName.Text))
            {
                await DisplayAlert("Błąd", "Użyj innej nazwy zdarzenie - ta nazwa juz jest wykorzystywana", "OK");

                return;
            } */

                evM.AddEventToDatabase();
                string eventNumber = evM.GetEventNumber();
                AmbulanceModel ambulance = evM.AddMajorAmbulanceToDatabase(AmbulanceNumber.Text, eventNumber, ChiefName.Text, ChiefSurname.Text);

                evM.CreateTriageDB();

                await DisplayAlert("TWÓJ KOD, ZAPISZ GO: ", ambulance.LoginCode, "OK");

                FileSystem fs = new FileSystem();
                fs.CreateDataFile(ambulance.LoginCode, UserInfo.EventId);

                await Navigation.PushAsync(new KAMPage());

            } catch (Exception ex)
            {
                await Navigation.PushAsync(new DatabaseErrorPage(ex.Message));
                return;
            }


        }
    }
}