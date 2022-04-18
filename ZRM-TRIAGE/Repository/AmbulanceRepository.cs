using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Linq;

using Firebase.Database.Query;

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
            _database.GetClient().Child("Crews").Child(UserInfo.EventId).PostAsync(item);
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

        public void Remove(string ambulanceNumber)
        {
            var ambulanceKey = SearchKey(ambulanceNumber);
            _database.GetClient().Child("Crews").Child(UserInfo.EventId).Child(ambulanceKey).DeleteAsync();
        }

        public AmbulanceModel Search(string ambulanceNumber)
        {
            var search = (_database.GetClient().Child("Crews").Child(UserInfo.EventId).OnceAsync<AmbulanceModel>().Result);

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
            var search = (_database.GetClient().Child("Crews").Child(UserInfo.EventId).OnceAsync<AmbulanceModel>().Result);

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

        public bool SearchFunction(AmbulanceModel.Function function)
        {
            bool isExists = false;

            if (function == AmbulanceModel.Function.Transport) 
                return false;

            var search = (_database.GetClient().Child("Crews").Child(UserInfo.EventId).OnceAsync<AmbulanceModel>().Result);

            foreach (var item in search)
            {
   
                if (item.Object.AmbulanceFunction == function)
                {
                    isExists = true;
                    break;
                }
            }

            return isExists;
        }

        public void Update(AmbulanceModel item)
        {
            var ambulanceKey = SearchKey(item.Number);


            _database.GetClient().Child("Crews").Child(UserInfo.EventId).Child(ambulanceKey).PutAsync(item);

        }
    }
}
