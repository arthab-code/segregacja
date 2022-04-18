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

        public EventRepository()
        {
            _database = new Database();
        }
        public void Add(EventModel item)
        {
            _database.GetClient().Child("Events").Child(item.EventId).PostAsync(item);
        }

        public EventModel Search(string eventId)
        {
            var search = (_database.GetClient().Child("Events").Child(eventId).OnceAsync<EventModel>().Result).FirstOrDefault();

            EventModel findEvent = null;

            findEvent = search?.Object;
            return findEvent;
        }

        public void Remove(string eventId)
        {
            _database.GetClient().Child("Events").Child(eventId).DeleteAsync();
        }

        public List<EventModel> GetAll()
        {
            throw new NotImplementedException();
        }

        public void Update(EventModel item)
        {
            throw new NotImplementedException();
        }

        public string SearchKey(string item)
        {
            throw new NotImplementedException();
        }
    }
}
