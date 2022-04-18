using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Firebase.Database.Query;

namespace ZRM_TRIAGE
{
    public class MainLoginProcess
    {
        private Database _database;
        private string _loginCode;
        private string _eventId;

        public MainLoginProcess(string loginCode)
        {
            _database = new Database();
            _loginCode = loginCode;

        }

        public bool IsLocalizedLoginCode()
        {
            var search = _database.GetClient().Child("LoginCodes").OnceAsync<LoginCodeModel>().Result;

            foreach(var item in search)
            {
                if (item.Object.LoginCode == _loginCode)
                {
                    _eventId = item.Object.EventId;
                    return true;
                }
            }

            return false;
        }

        public AmbulanceModel.Function RetrieveAmbulanceFunction()
        {
            var search = _database.GetClient().Child("Crews").Child(_eventId).OnceAsync<AmbulanceModel>().Result;

            AmbulanceModel.Function function = AmbulanceModel.Function.Transport;

            foreach(var item in search)
            {
                if (item.Object.LoginCode == _loginCode)
                {
                    function = item.Object.AmbulanceFunction;
                    break;
                }
            }

            return function;
        }

        public string GetEventId() => _eventId;
        public string GetLoginCode() => _loginCode;
    }
}
