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

        private EventRepository _eventRepos;
        Database _db = new Database();
       
        public EventCreateViewModel()
        {
            _eventRepos = new EventRepository();
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

            EventModel check = _eventRepos.Search(eventId);

            if (check != null)
                isExists = true;

            return isExists;
        }

        public void AddEventToDatabase(string eventId)
        {
            EventModel newEvent = new EventModel()
            {
                EventId = eventId
            };

            _eventRepos.Add(newEvent);
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

        public AmbulanceModel AddMajorAmbulanceToDatabase(string ambulanceNumber, string eventId)
        {
            UserInfo.EventId = eventId;
            UserInfo.AmbulanceNumber = ambulanceNumber;

            AmbulanceModel ambulance = ConfigureMajorAmbulance(ambulanceNumber, eventId);

            AmbulanceRepository ambulanceRepos = new AmbulanceRepository();

            ambulanceRepos.Add(ambulance);

            return ambulance;
        }

    }
}
