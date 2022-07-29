using System;
using System.Collections.Generic;
using System.Text;

using Firebase.Database.Query;
namespace ZRM_TRIAGE
{
    public class LoginCodeRepository
    {
        private Database<LoginCodeModel> _database;
        private string _dataName = "LoginCodes";

        public LoginCodeRepository()
        {
            _database = new Database<LoginCodeModel>();
        }
        public void Add(LoginCodeModel item)
        {
            //  _database.GetClient().Child(_dataName).PostAsync(item);
            _database.Create(_dataName, item);
        }

        public void Remove(string eventId)
        {
           // _database.GetClient().Child(_dataName).Child(eventId).DeleteAsync();
            _database.Delete(_dataName, eventId);
        }

    }
}
