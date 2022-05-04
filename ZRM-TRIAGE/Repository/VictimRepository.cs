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
            var firebaseClient = _database.GetClient().Child("Victims").Child(UserInfo.EventId).PostAsync(item).Result;
            item.Id = firebaseClient.Key;
            _database.GetClient().Child("Victims").Child(UserInfo.EventId).Child(item.Id).PutAsync(item);
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

        public void Remove(string victimId)
        {
            _database.GetClient().Child("Victims").Child(UserInfo.EventId).Child(victimId).DeleteAsync();
        }

        public VictimModel Search(string victimInfoParts)
        {
            var tmp = victimInfoParts.Split('.');
            string name, surname;
            name = tmp[0];
            surname = tmp[1];

            var search = _database.GetClient().Child("Victims").Child(UserInfo.EventId).OnceAsync<VictimModel>().Result;

            VictimModel findVictim = null;

            foreach (var item in search)
            {
                if (item.Object.Name == name && item.Object.Surname == surname)
                {
                    findVictim = item.Object;
                    break;
                }
            }

            return findVictim;
        }

        public string SearchKey(string victimInfoParts)
        {
            var tmp = victimInfoParts.Split('.');
            string name, surname;
            name = tmp[0];
            surname = tmp[1];
                
            var search = _database.GetClient().Child("Victims").Child(UserInfo.EventId).OnceAsync<VictimModel>().Result;

            string findKey = null;

            foreach (var item in search)
            {

                if (item.Object.Name == name && item.Object.Surname == surname)
                {
                    findKey = item.Key;
                    break;
                }
            }

            return findKey;
        }

        public void Update(VictimModel oldItem, VictimModel newItem)
        {
            _database.GetClient().Child("Victims").Child(UserInfo.EventId).Child(oldItem.Id).PutAsync(newItem);
        }
    }
}
