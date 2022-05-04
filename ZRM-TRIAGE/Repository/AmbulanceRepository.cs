using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Linq;

using Firebase.Database.Query;
using Firebase.Database;

namespace ZRM_TRIAGE
{
    public class AmbulanceRepository : IClientRepository<AmbulanceModel>
    {
        private Database _database;
        public AmbulanceRepository()
        {
            _database = new Database();
        }

        public void Add(AmbulanceModel item)
        {
            var firebaseClient = _database.GetClient().Child("Crews").Child(UserInfo.EventId).PostAsync(item).Result;

            item.Id = firebaseClient.Key;

            _database.GetClient().Child("Crews").Child(UserInfo.EventId).Child(item.Id).PutAsync(item);

            LoginCodeModel loginCodeModel = new LoginCodeModel();
            loginCodeModel.LoginCode = item.LoginCode;
            loginCodeModel.EventId = item.EventId;

            LoginCodeRepository lCr = new LoginCodeRepository();

            lCr.Add(loginCodeModel);
        }

        public List<AmbulanceModel> GetAll()
        {
            var list = _database.GetClient().Child("Crews").Child(UserInfo.EventId).OnceAsync<AmbulanceModel>().Result;

            List<AmbulanceModel> ambulanceList = new List<AmbulanceModel>();

            foreach (var item in list)
            {
                ambulanceList.Add(item.Object);
            }

            return ambulanceList;
        }


        public void Remove(string ambulanceId)
        {
            _database.GetClient().Child("Crews").Child(UserInfo.EventId).Child(ambulanceId).DeleteAsync();
        }

        public AmbulanceModel Search(string ambulanceNumber)
        {
            var search = _database.GetClient().Child("Crews").Child(UserInfo.EventId).OnceAsync<AmbulanceModel>().Result;

            AmbulanceModel findAmbulance = null;

            foreach(var item in search)
            {
                if (item.Object.Number == ambulanceNumber)
                {
                    findAmbulance = item.Object;
                    break;
                }
            }

            return findAmbulance;
        }

        public string SearchKey(string ambulanceNumber)
        {
            var search = _database.GetClient().Child("Crews").Child(UserInfo.EventId).OnceAsync<AmbulanceModel>().Result;

            string findAmbulanceKey = null;

            foreach (var item in search)
            {
                findAmbulanceKey = null;

                if (item.Object.Number == ambulanceNumber)
                {
                    findAmbulanceKey = item.Key;
                    break;
                }
            }

            return findAmbulanceKey;
        }


        public void Update(AmbulanceModel oldItem, AmbulanceModel newItem)
        {
            _database.GetClient().Child("Crews").Child(UserInfo.EventId).Child(oldItem.Id).PutAsync(newItem);

        }

        public Database GetDatabase()
        {
            return _database;
        }

    }
}
