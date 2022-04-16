using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

using Firebase.Database.Query;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Firebase.Database;

namespace ZRM_TRIAGE
{
    internal class EventCreateViewModel
    {

        private Database _db;
       
        public EventCreateViewModel()
        {
            _db = new Database();
        }


        public bool CheckCorrectFormFields(string a, string b)
        {
            bool isCorrect = true;

            if (String.IsNullOrWhiteSpace(a) || String.IsNullOrWhiteSpace(b))
            {
                isCorrect = false;
            }

            return isCorrect;
        }

        public bool CheckEventExists(string eventId)
        {
            bool isExists = false;

            /* niebezpieczne miejsce w programie */
            var thisSame = _db.GetClient()
                .Child("Crews")
                .OnceAsync<EventModel>().Result;

             foreach (var item in thisSame)
             {
                 if (item.Object.EventId == eventId && isExists == false)
                 {
                     isExists = true;                    
                     break;
                 }
             }

            return isExists;
        }

        public async void AddEventToDatabase(string eventId)
        {
            await _db.GetClient().Child("Events").PostAsync(new EventModel()
            {
                EventId = eventId
            });
        }

        private AmbulanceModel ConfigureMajorAmbulance(string ambulanceNumber, string eventId)
        {
            AmbulanceBuilderModel ambulanceBuilder = new AmbulanceBuilderModel();

            AmbulanceModel ambulance = ambulanceBuilder
                .AmbulanceSetNumber(ambulanceNumber)
                .AmbulanceSetEventId(eventId)
                .LoginCodeGenerate()
                .AmbulanceFunctionSet(AmbulanceModel.Function.Major)
                .AmbulanceStatusSet()
                .AmbulanceHospitalSet()
                .AmbulanceVictimSet()
                .Build();

            return ambulance;
        }

        public async void AddMajorAmbulanceToDatabase(string ambulanceNumber, string eventId)
        {
            AmbulanceModel ambulance = ConfigureMajorAmbulance(ambulanceNumber, eventId);

            UserInfo.EventId = eventId;

            await _db.GetClient().Child("Crews").PostAsync(ambulance);
        }

    }
}
