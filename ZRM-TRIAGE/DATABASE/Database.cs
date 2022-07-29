using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Firebase;
using Firebase.Database;
using Firebase.Database.Query;

namespace ZRM_TRIAGE
{
    public class Database<T> : IDatabase<T> where T : class
    {
        private string _connectionString = "https://zrm-triage-test-bc88f-default-rtdb.europe-west1.firebasedatabase.app/";
        private FirebaseClient _fbClient;

        public Database()
        {
            _fbClient = new FirebaseClient(_connectionString);
        }

        public void Create(string dbName, string eventId, T record)
        {
            var obj = GetClient().Child(dbName).Child(eventId).PostAsync(record).Result;

            var recordKey = obj.Key;

            var componentObject = record as Component;
            componentObject.DatabaseId = recordKey;

            GetClient().Child(dbName).Child(eventId).Child(recordKey).PutAsync(componentObject);
        }

        public void Create(string dbName,  T record)
        {
            var obj = GetClient().Child(dbName).PostAsync(record).Result;

            var recordKey = obj.Key;

            var componentObject = record as Component;
            componentObject.DatabaseId = recordKey;

            GetClient().Child(dbName).Child(recordKey).PutAsync(componentObject);
        }

        public void Delete(string dbName, string eventId, string recordKey)
        {
            GetClient().Child(dbName).Child(eventId).Child(recordKey).DeleteAsync();
        }

        public void Delete(string dbName, string eventId)
        {
            GetClient().Child(dbName).Child(eventId).DeleteAsync();
        }

        public T Read(string dbName, string eventId, string recordKey)
        {
            var tmp = GetClient().Child(dbName).Child(eventId).Child(recordKey).OnceSingleAsync<T>().Result;

            return tmp;
        }

        public T Read(string dbName, string eventId)
        {
            var tmp = GetClient().Child(dbName).Child(eventId).OnceAsync<T>().Result.FirstOrDefault();

            return tmp.Object;
        }

        public T Read(string dbName)
        {
            var tmp =  GetClient().Child(dbName).OnceAsync<T>().Result.FirstOrDefault();

            return tmp?.Object;
        }


        public List<T> ReadAll(string dbName, string eventId)
        {
            List<T> values = new List<T>();

            var tmp = GetClient().Child(dbName).Child(eventId).OnceAsync<T>().Result.ToList();

            foreach (var item in tmp)
                values.Add(item.Object);

            return values;
        }

        public void Update(string dbName, string eventId, string recordKey, T record)
        {
            GetClient().Child(dbName).Child(eventId).Child(recordKey).PutAsync(record);
        }

        public FirebaseClient GetClient() => _fbClient;

    }
}
