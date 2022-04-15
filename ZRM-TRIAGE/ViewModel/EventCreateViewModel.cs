using System;
using System.Collections.Generic;
using System.Text;

using Firebase.Database.Query;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Firebase.Database;

namespace ZRM_TRIAGE
{
    internal class EventCreateViewModel
    {

        private Database db;
       
        public EventCreateViewModel()
        {
            db = new Database();
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

        public bool CheckEventExists(string eventName)
        {
            bool isExists = false;

            var thisSame = db.GetClient().Child("Events").OnceAsync<EventModel>().Result;

            foreach (var item in thisSame)
            {
                if (item.Object.Name == eventName && isExists == false)
                {
                    isExists = true;
                    break;
                }
            }
            return isExists;
        }

        public async void AddEventToDatabase(string eventName)
        {
            await db.GetClient().Child("Events").PostAsync(new EventModel()
            {
                Name = eventName
            });
        }

        private AmbulanceModel ConfigureMajorAmbulance(string ambulanceNumber, string eventName)
        {
            AmbulanceBuilderModel ambulanceBuilder = new AmbulanceBuilderModel();

            AmbulanceModel ambulance = ambulanceBuilder
                .AmbulanceSetNumber(ambulanceNumber)
                .AmbulanceSetEventId(eventName)
                .LoginCodeGenerate()
                .AmbulanceFunctionSet("Major")
                .AmbulanceStatusSet()
                .AmbulanceHospitalSet()
                .AmbulanceVictimSet()
                .Build();

            return ambulance;
        }

        public async void AddMajorAmbulanceToDatabase(string ambulanceNumber, string eventName)
        {
            AmbulanceModel ambulance = ConfigureMajorAmbulance(ambulanceNumber, eventName);

            await db.GetClient().Child("Crews").PostAsync(ambulance);
        }

    }
}
