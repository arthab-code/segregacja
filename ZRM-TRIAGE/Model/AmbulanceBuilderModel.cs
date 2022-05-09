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
        private Database _database;

        public AmbulanceBuilderModel()
        {
            _ambulanceModel = new AmbulanceModel();
            _database = new Database();
        }

        public AmbulanceBuilderModel AmbulanceSetNumber(string number)
        {
            _ambulanceModel.Number = number;

            return this;
        }

        public AmbulanceBuilderModel AmbulanceSetEventId(string eventId)
        {
            _ambulanceModel.EventId = eventId;

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

            var thisSame = _database.GetClient()
                .Child("Crews")
                .Child(UserInfo.EventId)
                .OnceAsync<AmbulanceModel>().Result;

            foreach (var item in thisSame)
            {
                if (item.Object.LoginCode == loginCode && isExists == false)
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
            _ambulanceModel.Hospital = null;

            return this;
        }

        public AmbulanceModel Build()
        {
            return _ambulanceModel;
        }
    }
}
