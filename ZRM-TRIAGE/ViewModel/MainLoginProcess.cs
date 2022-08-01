using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Firebase.Database.Query;

namespace ZRM_TRIAGE
{
    public class MainLoginProcess
    {
        private string _loginCode;
        private string _eventId;

        public MainLoginProcess(string loginCode)
        {
            _loginCode = loginCode;

        }

        public bool IsLocalizedLoginCode()
        {
            Database<LoginCodeModel> databaseLoginCodeModel = new Database<LoginCodeModel>();
            //var search = _database.GetClient().Child("LoginCodes").OnceAsync<LoginCodeModel>().Result;
            var search = databaseLoginCodeModel.ReadAll("LoginCodes");

            foreach(var item in search)
            {
                if (item.LoginCode == _loginCode)
                {
                    _eventId = item.EventId;
                    return true;
                }
            }

            return false;
        }

            public AmbulanceModel.Function RetrieveAmbulanceFunction(string eventId)
            {

            Database<AmbulanceModel> databaseAmbulanceModel = new Database<AmbulanceModel>();

            //  var search = _database.GetClient().Child("Crews").Child(eventId).OnceAsync<AmbulanceModel>().Result;
            var search = databaseAmbulanceModel.ReadAll("Crews", eventId);

                AmbulanceModel.Function function = AmbulanceModel.Function.Transport;

                foreach(var item in search)
                {
                    if (item.LoginCode == _loginCode)
                    {
                        function = item.AmbulanceFunction;
                        break;
                    }
                }

                return function;
            }

        public AmbulanceModel RetrieveAmbulance(string eventId)
        {
            Database<AmbulanceModel> databaseAmbulanceModel = new Database<AmbulanceModel>();
            // var search = _database.GetClient().Child("Crews").Child(eventId).OnceAsync<AmbulanceModel>().Result;
            var search = databaseAmbulanceModel.ReadAll("Crews", eventId);
            AmbulanceModel ambulance = null;

            foreach (var item in search)
            {
                if (item.LoginCode == _loginCode)
                {
                    ambulance = item;
                    break;
                }
            }

            return ambulance;
        }

        public string GetEventId() => _eventId;
        public string GetLoginCode() => _loginCode;
    }
}
