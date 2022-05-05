using Firebase.Database;
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

        public AmbulanceModel (AmbulanceModel original)
        {
            this.Number = original.Number;
            this.LoginCode = original.LoginCode;
            this.EventId = original.EventId;
            this.Id = original.Id;
            this.Victim = original.Victim;
            this.Hospital = original.Hospital;
            this.AmbulanceStatus = original.AmbulanceStatus;
            this.AmbulanceFunction = original.AmbulanceFunction;
        }

        public AmbulanceModel()
        { }

        public string Number { get; set; }
        public string KeyString { get; set; }
        public string LoginCode { get;  set; }
        public string EventId { get; set; }
        public string Id { get; set; }
        public VictimModel Victim { get; set; }
        public HospitalModel Hospital { get; set; }
        public Status AmbulanceStatus { get; set; }
        public Function AmbulanceFunction { get; set; }

    }
}
