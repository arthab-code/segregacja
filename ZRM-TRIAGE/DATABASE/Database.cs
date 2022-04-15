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
        private string _connectionString = "https://test-62d49-default-rtdb.europe-west1.firebasedatabase.app/";
        private FirebaseClient _fbClient;

        public Database()
        {
            _fbClient = new FirebaseClient(_connectionString);
        }

        public FirebaseClient GetClient() => _fbClient;
    }
}
