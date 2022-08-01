using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Linq;

using Firebase.Database.Query;
using Firebase.Database;

namespace ZRM_TRIAGE
{
    public class AmbulanceRepository 
    {
        private Database<AmbulanceModel> _database;
        private string _dataName = "Crews";
        public AmbulanceRepository()
        {
            _database = new Database<AmbulanceModel>();
        }

        public void Add(AmbulanceModel item)
        {
            /* var firebaseClient = _database.GetClient().Child(_dataName).Child(UserInfo.EventId).PostAsync(item).Result;

             item.DatabaseId = firebaseClient.Key; 

             _database.GetClient().Child(_dataName).Child(UserInfo.EventId).Child(item.DatabaseId).PutAsync(item); */

            _database.Create(_dataName, UserInfo.EventId, item);

            LoginCodeModel loginCodeModel = new LoginCodeModel();
            loginCodeModel.LoginCode = item.LoginCode;
            loginCodeModel.EventId = UserInfo.EventId;

            LoginCodeRepository lCr = new LoginCodeRepository();

            lCr.Add(loginCodeModel);
        }

        public List<AmbulanceModel> GetAll()
        {
            //var list = _database.GetClient().Child(_dataName).Child(UserInfo.EventId).OnceAsync<AmbulanceModel>().Result;
            var list = _database.ReadAll(_dataName, UserInfo.EventId);

            List<AmbulanceModel> ambulanceList = new List<AmbulanceModel>();

            foreach (var item in list)
            {
                ambulanceList.Add(item);
            }

            return ambulanceList;
        }


        public void Remove(string ambulanceId)
        {
           // _database.GetClient().Child(_dataName).Child(UserInfo.EventId).Child(ambulanceId).DeleteAsync();
            _database.Delete(_dataName, UserInfo.EventId, ambulanceId);

        }

        public AmbulanceModel GetAmbulance(string ambulanceId)
        {
            //var result = _database.GetClient().Child(_dataName).Child(UserInfo.EventId).Child(ambulanceId).OnceSingleAsync<AmbulanceModel>().Result;
            var result = _database.Read(_dataName, UserInfo.EventId, ambulanceId);

            return result;
        }


        public void Update(AmbulanceModel ambulance)
        {
            _database.Update(_dataName, UserInfo.EventId,ambulance.DatabaseId,ambulance);

        }

        public Database<AmbulanceModel> GetDatabase()
        {
            return _database;
        }

    }
}
