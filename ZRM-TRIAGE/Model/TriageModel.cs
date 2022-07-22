using System;
using System.Collections.Generic;
using System.Text;

namespace ZRM_TRIAGE
{
    public class TriageModel
    {
        public int Red { get; set; }
        public int Yellow { get; set; }
        public int Green { get; set; }
        public int Black { get; set; }
        public int Amount { get; set; }

        public TriageModel()
        {
            Red = 0;
            Yellow = 0;
            Green = 0;
            Black = 0;
            Amount = 0;
        }

        private static TriageModel _instance;

        public static TriageModel Instance { 

            get
            {
                if (_instance == null)
                    _instance = new TriageModel();
                
                return _instance;
            }

            set { _instance = value; }
     
        }
    }
}
