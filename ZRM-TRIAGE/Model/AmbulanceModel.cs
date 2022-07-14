using Firebase.Database;
using System;
using System.Collections.Generic;
using System.Text;

namespace ZRM_TRIAGE
{
    public class AmbulanceModel : Component
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
        public string LoginCode { get; set; }
      //  public string EventId { get; set; }
        public List<VictimModel> Victims { get; set; }
        public string HospitalId { get; set; }
        public string HospitalName { get; set; }
        public HospitalModel SelectedHospital { get; set; }
        public Status AmbulanceStatus { get; set; }
        public Function AmbulanceFunction { get; set; }
        public AmbulanceModel (AmbulanceModel original)
        {
            Victims = new List<VictimModel>();
            this.Number = original.Number;
            this.LoginCode = original.LoginCode;
           // this.EventId = original.EventId;
            this.Id = original.Id;
            this.Victims = original.Victims;
            this.HospitalId = original.HospitalId;
            this.AmbulanceStatus = original.AmbulanceStatus;
            this.AmbulanceFunction = original.AmbulanceFunction;
        } 

        public AmbulanceModel()
        {
            Victims = new List<VictimModel>();
        }

    }
}
