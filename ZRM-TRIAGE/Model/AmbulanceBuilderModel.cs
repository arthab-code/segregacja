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

        public AmbulanceBuilderModel()
        {
            _ambulanceModel = new AmbulanceModel();
        }

        public AmbulanceBuilderModel AmbulanceSetNumber(string number)
        {
            _ambulanceModel.Number = number;

            return this;
        }

        public AmbulanceBuilderModel LoginCodeGenerate()
        {
            //GENERATE CODE LOGICAL

            _ambulanceModel.LoginCode = "12345";

            return this;
        }

        private bool LoginCodeChecking()
        {
            bool score = true;
            //CHECKING CONFIRM LOGIN CODE

            return score;
        }

        public AmbulanceBuilderModel AmbulanceStatusSet()
        {
            _ambulanceModel.AmbulanceStatus = AmbulanceModel.Status.AtLocation;

            return this;
        }

        public AmbulanceBuilderModel AmbulanceFunctionSet(string function)
        {

            switch(function)
            {
                case "Strefa Czerwona":
                    _ambulanceModel.AmbulanceFunction = AmbulanceModel.Function.Red;
                    //check whether exists this function
                    break;

                case "Strefa Żółta":
                    _ambulanceModel.AmbulanceFunction = AmbulanceModel.Function.Yellow;
                    //check whether exists this function
                    break;

                case "Strefa Zielona":
                    _ambulanceModel.AmbulanceFunction = AmbulanceModel.Function.Green;
                    //check whether exists this function
                    break;

                case "ZRM":
                    _ambulanceModel.AmbulanceFunction = AmbulanceModel.Function.Transport;
                    break;
            }

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
