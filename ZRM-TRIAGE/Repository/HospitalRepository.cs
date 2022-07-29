using Firebase.Database.Query;
using System;
using System.Collections.Generic;
using System.Text;

namespace ZRM_TRIAGE
{
    public class HospitalRepository 
    {
        private Database<HospitalModel> _database;
        public string _dataName { get => "Hospitals"; private set { } }
        public HospitalRepository()
        {
            _database = new Database<HospitalModel>();
        }

        public void Add(HospitalModel item)
        {
            //  var firebaseObject = _database.GetClient().Child(_dataName).Child(UserInfo.EventId).PostAsync(item).Result;

            //   item.DatabaseId = firebaseObject.Key;

            // _database.GetClient().Child(_dataName).Child(UserInfo.EventId).Child(item.DatabaseId).PutAsync(item);

            _database.Create(_dataName, UserInfo.EventId, item);


        }

        public List<HospitalModel> GetAll()
        {
            //  var list = _database.GetClient().Child(_dataName).Child(UserInfo.EventId).OnceAsync<HospitalModel>().Result;

            var list = _database.ReadAll(_dataName, UserInfo.EventId);

            List<HospitalModel> hospitalList = new List<HospitalModel>();

            foreach (var item in list)
            {
                hospitalList.Add(item);
            }

            return hospitalList;
        }

        public void Remove(string recordKey)
        {
           // _database.GetClient().Child(_dataName).Child(UserInfo.EventId).Child(item).DeleteAsync();
           _database.Delete(_dataName, UserInfo.EventId, recordKey);
        }

        public HospitalModel Search(string hospitalId)
        {
            //var result = _database.GetClient().Child(_dataName).Child(UserInfo.EventId).Child(hospitalId).OnceSingleAsync<HospitalModel>().Result;
            var result = _database.Read(_dataName, UserInfo.EventId, hospitalId);
            return result;
        }

        public void Update(HospitalModel hospital)
        {
            //_database.GetClient().Child(_dataName).Child(UserInfo.EventId).Child(oldItem.DatabaseId).PutAsync(newItem);
            _database.Update(_dataName, UserInfo.EventId, hospital.DatabaseId, hospital);
        }
    }
}
