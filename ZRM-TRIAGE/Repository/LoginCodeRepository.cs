using System;
using System.Collections.Generic;
using System.Text;

using Firebase.Database.Query;
namespace ZRM_TRIAGE
{
    public class LoginCodeRepository : IClientRepository<LoginCodeModel>
    {
        private Database _database;

        public LoginCodeRepository()
        {
            _database = new Database();
        }
        public void Add(LoginCodeModel item)
        {
            _database.GetClient().Child("LoginCodes").PostAsync(item);
        }

        public List<LoginCodeModel> GetAll()
        {
            throw new NotImplementedException();
        }

        public void Remove(string item)
        {
            throw new NotImplementedException();
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
