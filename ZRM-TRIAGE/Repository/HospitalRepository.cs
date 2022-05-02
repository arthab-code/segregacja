﻿using Firebase.Database.Query;
using System;
using System.Collections.Generic;
using System.Text;

namespace ZRM_TRIAGE
{
    public class HospitalRepository : IClientRepository<HospitalModel>
    {
        private Database _database;

        public HospitalRepository()
        {
            _database = new Database();
        }

        public void Add(HospitalModel item)
        {
            _database.GetClient().Child("Hospitals").Child(UserInfo.EventId).PostAsync(item);
        }

        public List<HospitalModel> GetAll()
        {
            var list = _database.GetClient().Child("Hospitals").Child(UserInfo.EventId).OnceAsync<HospitalModel>().Result;

            List<HospitalModel> hospitalList = new List<HospitalModel>();

            foreach (var item in list)
            {
                hospitalList.Add(item.Object);
            }

            return hospitalList;
        }

        public void Remove(string item)
        {
            string searchKey = SearchKey(item);
            _database.GetClient().Child("Hospitals").Child(UserInfo.EventId).Child(searchKey).DeleteAsync();
        }

        public HospitalModel Search(string item)
        {
            var search = _database.GetClient().Child("Hospitals").Child(UserInfo.EventId).OnceAsync<HospitalModel>().Result;

            HospitalModel findHospital = null;

            foreach (var i in search)
            {
                if (i.Object.Name == item)
                {
                    findHospital = i.Object;
                    break;
                }
            }

            return findHospital;
        }

        public string SearchKey(string item)
        {
            var search = _database.GetClient().Child("Hospitals").Child(UserInfo.EventId).OnceAsync<HospitalModel>().Result;
            string result = "";
            foreach(var i in search)
            {
                if (i.Object.Name == item)
                {
                    result = i.Key;
                    break;
                }
            }

            return result;
        }

        public void Update(HospitalModel oldItem, HospitalModel newItem)
        {
            var hospitalKey = SearchKey(oldItem.Name);

            _database.GetClient().Child("Hospitals").Child(UserInfo.EventId).Child(hospitalKey).PutAsync(newItem);
        }
    }
}