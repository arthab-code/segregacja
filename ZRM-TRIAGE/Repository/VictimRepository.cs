using Firebase.Database.Query;
using System;
using System.Collections.Generic;
using System.Text;

namespace ZRM_TRIAGE
{
    public class VictimRepository : IClientRepository<VictimModel>
    {
        private Database _database;

        public VictimRepository()
        {
            _database = new Database(); 
        }
        public void Add(VictimModel item)
        {
            _database.GetClient().Child("Victims").Child(UserInfo.EventId).PostAsync(item);
        }

        public List<VictimModel> GetAll()
        {
            var list = _database.GetClient().Child("Victims").Child(UserInfo.EventId).OnceAsync<VictimModel>().Result;

            List<VictimModel> victimList = new List<VictimModel>();

            foreach (var item in list)
            {
                victimList.Add(item.Object);
            }

            return victimList;
        }

        public void Remove(string item)
        {
            throw new NotImplementedException();
        }

        public VictimModel Search(string item)
        {
            throw new NotImplementedException();
        }

        public string SearchKey(string item1)
        {
            throw new NotImplementedException();
        }

        public void Update(VictimModel oldItem, VictimModel newItem)
        {
            throw new NotImplementedException();
        }
    }
}
