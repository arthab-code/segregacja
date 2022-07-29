using Firebase.Database.Query;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace ZRM_TRIAGE
{
    public class VictimRepository 
    {
        private Database<VictimModel> _database;
        public string _dataName { get => "Victims"; private set { } }

        public VictimRepository()
        {
            _database = new Database<VictimModel>(); 
        }
        public void Add(VictimModel item)
        {
            //var firebaseClient = _database.GetClient().Child(_dataName).Child(UserInfo.EventId).PostAsync(item).Result;
            //item.DatabaseId = firebaseClient.Key;
            //_database.GetClient().Child(_dataName).Child(UserInfo.EventId).Child(item.DatabaseId).PutAsync(item);

            _database.Create(_dataName, UserInfo.EventId, item);
        }

        public List<VictimModel> GetAll()
        {
            // var list = _database.GetClient().Child(_dataName).Child(UserInfo.EventId).OnceAsync<VictimModel>().Result;

            var list = _database.ReadAll(_dataName, UserInfo.EventId);

            List<VictimModel> victimList = new List<VictimModel>();

            foreach (var item in list)
            {
                victimList.Add(item);
            }

            return victimList;
        } 


        public void Remove(string victimId)
        {
            //_database.GetClient().Child(_dataName).Child(UserInfo.EventId).Child(victimId).DeleteAsync();
            _database.Delete(_dataName,UserInfo.EventId,victimId);
        }

        public VictimModel Search(string victimInfoParts)
        {
            var tmp = victimInfoParts.Split('.');
            string name, surname;
            name = tmp[0];
            surname = tmp[1];

            var search = _database.GetClient().Child(_dataName).Child(UserInfo.EventId).OnceAsync<VictimModel>().Result;

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

        public void Update(VictimModel victim)
        {
            // _database.GetClient().Child(_dataName).Child(UserInfo.EventId).Child(oldItem.DatabaseId).PutAsync(newItem);
            _database.Update(_dataName, UserInfo.EventId, victim.DatabaseId, victim);
        }
    }
}
