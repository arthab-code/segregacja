using System;
using System.Collections.Generic;
using System.Text;

namespace ZRM_TRIAGE
{
    public class AmbulanceModel
    {
        public enum Status
        {
            AtLocation,
            Transporting,
            InHospital
        }

        public enum Function
        {
            Major,
            Red,
            Yellow,
            Green,
            Transport
        }

        public string Number { get; set; }
        public string LoginCode { get;  set; }
        public string EventId { get; set; }
        public VictimModel Victim { get; set; }
        public HospitalModel Hospital { get; set; }
        public Status AmbulanceStatus { get; set; }
        public Function AmbulanceFunction { get; set; }

    }
}
