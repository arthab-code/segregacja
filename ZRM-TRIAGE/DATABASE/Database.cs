using System;
using System.Collections.Generic;
using System.Text;
using Firebase;
using Firebase.Database;
using Firebase.Database.Query;

namespace ZRM_TRIAGE
{
    public class Database
    {
        private string _connectionString = "https://zrm-triage-test-54325-default-rtdb.europe-west1.firebasedatabase.app/";
        private FirebaseClient _fbClient;

        public Database()
        {
            _fbClient = new FirebaseClient(_connectionString);
        }

        public FirebaseClient GetClient() => _fbClient;
    }
}
