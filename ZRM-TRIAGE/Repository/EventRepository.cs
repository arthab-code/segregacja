using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Firebase.Database.Query;
using System.Threading.Tasks;

namespace ZRM_TRIAGE
{
    public class EventRepository 
    {
        private Database<EventModel> _databaseEventModel;
        private Database<EventCounter> _databaseEventCounter;
        private string _dataName = "Events";
        private string _eventCounter = "EventCounter";
        public EventRepository()
        {
            _databaseEventModel = new Database<EventModel>();
            _databaseEventCounter = new Database<EventCounter>();
        }
        public void Add(EventModel item)
        {
            // _database.GetClient().Child(_dataName).Child(item.EventNumber).PostAsync(item);
            _databaseEventModel.Create(_dataName, item.EventNumber, item);
        }

        public EventModel Search(string eventId)
        {
            //var search = (_database.GetClient().Child(_dataName).Child(eventId).OnceAsync<EventModel>().Result).FirstOrDefault();

            EventModel search = _databaseEventModel.Read(_dataName, eventId);

            EventModel findEvent = null;

            findEvent = search;
            return findEvent;
        }

        public string GetEventCounter()
        {
           // var r = (_databaseEventCounter.GetClient().Child(_eventCounter).OnceAsync<EventCounter>().Result).FirstOrDefault();

            var r = _databaseEventCounter.Read(_eventCounter);

            var result = new EventCounter() { eventCounter = r?.eventCounter };
            string a = null;

            if (result.eventCounter != null)
                a = result.eventCounter;

            return a;
        }

        public  string CreateNewEventCounter()
        {
          //  var r = (_databaseEventCounter.GetClient().Child(_eventCounter).OnceAsync<EventCounter>().Result).FirstOrDefault();

            EventCounter r = _databaseEventCounter.Read(_eventCounter);

            var result = new EventCounter() { 
                eventCounter = r?.eventCounter,
                DatabaseId = r?.DatabaseId
            };

            string a = null;
            if (result.eventCounter == null)
            {
                result = new EventCounter();
                a = "1";
                result.eventCounter = a;
                // _databaseEventCounter.GetClient().Child(_eventCounter).PostAsync(result);
                _databaseEventCounter.Create(_eventCounter, result);
               
            } else
            {
              //  string key = r.Key;
                int tmp = Int32.Parse(result.eventCounter);
                tmp++;
                a = tmp.ToString();
                result.eventCounter = a;
                _databaseEventCounter.GetClient().Child(_eventCounter).Child(result.DatabaseId).PutAsync(result);
            }
            return a;
        }

        public void Remove(string eventId)
        {
            // _databaseEventModel.GetClient().Child(_dataName).Child(eventId).DeleteAsync();
            _databaseEventCounter.Delete(_dataName, eventId);
        }
    }
}
