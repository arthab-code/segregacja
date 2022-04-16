using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ZRM_TRIAGE
{
    public class AmbulanceBuilderModel
    {
        private AmbulanceModel _ambulanceModel;
        private Database _db;

        public AmbulanceBuilderModel()
        {
            _ambulanceModel = new AmbulanceModel();
            _db = new Database();
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

            /* niebezpieczne miejsce w programie */
            var thisSame = _db.GetClient()
                .Child("Crews")
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
            /*switch(function)
            {
                case "Major":
                    _ambulanceModel.AmbulanceFunction = AmbulanceModel.Function.Major;
                    break;

                case "Red":
                    _ambulanceModel.AmbulanceFunction = AmbulanceModel.Function.Red;
                    //check whether exists this function
                    break;

                case "Yellow":
                    _ambulanceModel.AmbulanceFunction = AmbulanceModel.Function.Yellow;
                    //check whether exists this function
                    break;

                case "Green":
                    _ambulanceModel.AmbulanceFunction = AmbulanceModel.Function.Green;
                    //check whether exists this function
                    break;

                case "Transport":
                    _ambulanceModel.AmbulanceFunction = AmbulanceModel.Function.Transport;
                    break;
            } */

            return this;
        }

        private bool AmbulanceFunctionCheck()
        {
            bool score = true;

            //Checking whether function exists

            return score;
        }

        public AmbulanceBuilderModel AmbulanceHospitalSet()
        {
            _ambulanceModel.Hospital = null;

            return this;
        }

        public AmbulanceBuilderModel AmbulanceVictimSet()
        {
            _ambulanceModel.Victim = null;

            return this;
        }

        public AmbulanceModel Build()
        {
            return _ambulanceModel;
        }
    }
}
