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


        public bool CheckCorrectFormFields(string a)
        {
            bool isCorrect = true;

            if (String.IsNullOrWhiteSpace(a))
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

        public void AddEventToDatabase()
        {
            EventModel newEvent = new EventModel();
            newEvent.EventNumber = _eventRepos.CreateNewEventCounter();
            _eventRepos.Add(newEvent);
        }

        private AmbulanceModel ConfigureMajorAmbulance(string ambulanceNumber, string eventId)
        {
            AmbulanceBuilderModel ambulanceBuilder = new AmbulanceBuilderModel();

            AmbulanceModel ambulance = ambulanceBuilder
                .AmbulanceSetNumber(ambulanceNumber)
               // .AmbulanceSetEventId(eventId)
                .LoginCodeGenerate()
                .AmbulanceFunctionSet(AmbulanceModel.Function.Major)
                .AmbulanceStatusSet()
                .AmbulanceHospitalSet()
                .Build();

            return ambulance;
        }

        public string GetEventNumber()
        {
            return _eventRepos.GetEventCounter();
        }

        public AmbulanceModel AddMajorAmbulanceToDatabase(string ambulanceNumber, string eventId)
        {
            UserInfo.SetEventId(eventId);
            UserInfo.SetAmbulanceNumber(ambulanceNumber);

            AmbulanceModel ambulance = ConfigureMajorAmbulance(ambulanceNumber, eventId);

            AmbulanceRepository ambulanceRepos = new AmbulanceRepository();

            ambulanceRepos.Add(ambulance);


            UserInfo.SetAmbulance(ambulance);

            return ambulance;
        }

        public void CreateTriageDB()
        {
            TriageRepository triageRepos = new TriageRepository();
            triageRepos.InitializeDB();
        }

    }
}
