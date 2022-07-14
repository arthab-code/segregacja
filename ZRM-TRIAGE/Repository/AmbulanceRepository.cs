﻿using System;
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
        private string _dataName = "Crews";
        public AmbulanceRepository()
        {
            _database = new Database();
        }

        public void Add(AmbulanceModel item)
        {
            var firebaseClient = _database.GetClient().Child(_dataName).Child(UserInfo.EventId).PostAsync(item).Result;

            item.Id = firebaseClient.Key;

            _database.GetClient().Child(_dataName).Child(UserInfo.EventId).Child(item.Id).PutAsync(item);

            LoginCodeModel loginCodeModel = new LoginCodeModel();
            loginCodeModel.LoginCode = item.LoginCode;
            loginCodeModel.EventId = UserInfo.EventId;

            LoginCodeRepository lCr = new LoginCodeRepository();

            lCr.Add(loginCodeModel);
        }

        public List<AmbulanceModel> GetAll()
        {
            var list = _database.GetClient().Child(_dataName).Child(UserInfo.EventId).OnceAsync<AmbulanceModel>().Result;

            List<AmbulanceModel> ambulanceList = new List<AmbulanceModel>();

            foreach (var item in list)
            {
                ambulanceList.Add(item.Object);
            }

            return ambulanceList;
        }


        public void Remove(string ambulanceId)
        {
            _database.GetClient().Child(_dataName).Child(UserInfo.EventId).Child(ambulanceId).DeleteAsync();
        }

        public AmbulanceModel Search(string ambulanceId)
        {
            var result = _database.GetClient().Child(_dataName).Child(UserInfo.EventId).Child(ambulanceId).OnceSingleAsync<AmbulanceModel>().Result;

            return result;
        }

        public string SearchKey(string ambulanceNumber)
        {
            var search = _database.GetClient().Child(_dataName).Child(UserInfo.EventId).OnceAsync<AmbulanceModel>().Result;

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
            _database.GetClient().Child(_dataName).Child(UserInfo.EventId).Child(oldItem.Id).PutAsync(newItem);

        }

        public Database GetDatabase()
        {
            return _database;
        }

    }
}
