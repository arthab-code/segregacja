using System;
using System.Collections.Generic;
using System.Text;

using Firebase.Database.Query;
namespace ZRM_TRIAGE
{
    public class LoginCodeRepository : IClientRepository<LoginCodeModel>
    {
        private Database _database;
        private string _dataName = "LoginCodes";

        public LoginCodeRepository()
        {
            _database = new Database();
        }
        public void Add(LoginCodeModel item)
        {
            _database.GetClient().Child(_dataName).PostAsync(item);
        }

        public List<LoginCodeModel> GetAll()
        {
            throw new NotImplementedException();
        }

        public void Remove(string eventId)
        {
            _database.GetClient().Child(_dataName).Child(eventId).DeleteAsync();
        }

        public LoginCodeModel Search(string item)
        {
            throw new NotImplementedException();
        }

        public string SearchKey(string item)
        {
            throw new NotImplementedException();
        }

        public void Update(LoginCodeModel oldItem, LoginCodeModel newItem)
        {
            throw new NotImplementedException();
        }
    }
}
