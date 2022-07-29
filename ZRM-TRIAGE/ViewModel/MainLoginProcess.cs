using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Firebase.Database.Query;

namespace ZRM_TRIAGE
{
    public class MainLoginProcess
    {
        private Database<LoginCodeModel> _database;
        private string _loginCode;
        private string _eventId;

        public MainLoginProcess(string loginCode)
        {
            _database = new Database<LoginCodeModel>();
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

            public AmbulanceModel.Function RetrieveAmbulanceFunction(string eventId)
            {
                var search = _database.GetClient().Child("Crews").Child(eventId).OnceAsync<AmbulanceModel>().Result;

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

        public AmbulanceModel RetrieveAmbulance(string eventId)
        {
            var search = _database.GetClient().Child("Crews").Child(eventId).OnceAsync<AmbulanceModel>().Result;

            AmbulanceModel ambulance = null;

            foreach (var item in search)
            {
                if (item.Object.LoginCode == _loginCode)
                {
                    ambulance = item.Object;
                    break;
                }
            }

            return ambulance;
        }

        public string GetEventId() => _eventId;
        public string GetLoginCode() => _loginCode;
    }
}
