using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

using Firebase.Database.Query;

namespace ZRM_TRIAGE
{
    public class AmbulanceBuilderModel
    {
        private AmbulanceModel _ambulanceModel;
        private Database<AmbulanceModel> _database;

        public AmbulanceBuilderModel()
        {
            _ambulanceModel = new AmbulanceModel();
            _database = new Database<AmbulanceModel>();
        }

        public AmbulanceBuilderModel AmbulanceSetNumber(string number)
        {
            _ambulanceModel.Number = number;

            return this;
        }

       /* public AmbulanceBuilderModel AmbulanceSetEventId(string eventId)
        {
            _ambulanceModel.EventId = eventId;

            return this;
        } */

        public AmbulanceBuilderModel SetChiefPersonalData(string name, string surname)
        {
            _ambulanceModel.ChiefPersonalData = name + " " + surname;

            return this;
        }


        public AmbulanceBuilderModel LoginCodeGenerate()
        {
            //GENERATE CODE LOGICAL
            const int loginCodeLength = 6;
            char[] loginCode = new char[loginCodeLength];

            Random random = new Random();

            string addCodeValue;

            while (true)
            {
                addCodeValue = "";

                for (int i=0; i<loginCodeLength; i++)
                {
                    loginCode[i] = (char)random.Next(97, 122);
                    addCodeValue += loginCode[i];
                }

                if (LoginCodeChecking(addCodeValue))
                    continue;
                else
                    break;
            }

            _ambulanceModel.LoginCode = addCodeValue;

            return this;
        }

        private bool LoginCodeChecking(string loginCode)
        {
            bool isExists = false;

            /*  var thisSame = _database.GetClient()
                  .Child("Crews")
                  .Child(UserInfo.EventId)
                  .OnceAsync<AmbulanceModel>().Result; */

            var thisSame = _database.ReadAll("Crews", UserInfo.EventId);

            foreach (var item in thisSame)
            {
                if (item.LoginCode == loginCode && isExists == false)
                {
                    isExists = true;
                    break;
                }
            }

            return isExists;
        }

        public AmbulanceBuilderModel AmbulanceStatusSet()
        {
            _ambulanceModel.AmbulanceStatus = AmbulanceModel.Status.AtLocation;

            return this;
        }

        public AmbulanceBuilderModel AmbulanceFunctionSet(AmbulanceModel.Function function)
        { 
            _ambulanceModel.AmbulanceFunction = function;

            return this;
        }

        public AmbulanceBuilderModel AmbulanceHospitalSet()
        {
            _ambulanceModel.HospitalId = null;

            return this;
        }

        public AmbulanceModel Build()
        {
            return _ambulanceModel;
        }
    }
}
