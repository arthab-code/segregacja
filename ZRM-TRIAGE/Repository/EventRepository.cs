using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Firebase.Database.Query;
using System.Threading.Tasks;

namespace ZRM_TRIAGE
{
    public class EventRepository : IClientRepository<EventModel>
    {
        private Database _database;
        private string _dataName = "Events";
        private string _eventCounter = "EventCounter";
        public EventRepository()
        {
            _database = new Database();
        }
        public void Add(EventModel item)
        {
            _database.GetClient().Child(_dataName).Child(item.EventNumber).PostAsync(item);
        }

        public EventModel Search(string eventId)
        {
            var search = (_database.GetClient().Child(_dataName).Child(eventId).OnceAsync<EventModel>().Result).FirstOrDefault();

            EventModel findEvent = null;

            findEvent = search?.Object;
            return findEvent;
        }

        public string GetEventCounter()
        {
            var r = (_database.GetClient().Child(_eventCounter).OnceAsync<EventCounter>().Result).FirstOrDefault();

            var result = new EventCounter() { eventCounter = r?.Object.eventCounter };
            string a = null;

            if (result.eventCounter != null)
                a = result.eventCounter;

            return a;
        }

        public  string CreateNewEventCounter()
        {
            var r = (_database.GetClient().Child(_eventCounter).OnceAsync<EventCounter>().Result).FirstOrDefault();

            var result = new EventCounter() { eventCounter = r?.Object.eventCounter };

            string a = null;
            if (result.eventCounter == null)
            {
                result = new EventCounter();
                a = "1";
                result.eventCounter = a;
                _database.GetClient().Child(_eventCounter).PostAsync(result);
               
            } else
            {
                string key = r.Key;
                int tmp = Int32.Parse(result.eventCounter);
                tmp++;
                a = tmp.ToString();
                result.eventCounter = a;
                _database.GetClient().Child(_eventCounter).Child(key).PutAsync(result);
            }
            return a;
        }

        public void Remove(string eventId)
        {
            _database.GetClient().Child(_dataName).Child(eventId).DeleteAsync();
        }

        public List<EventModel> GetAll()
        {
            throw new NotImplementedException();
        }

        public void Update(EventModel oldItem, EventModel newItem)
        {
            throw new NotImplementedException();
        }

        public string SearchKey(string item)
        {
            throw new NotImplementedException();
        }
    }
}
