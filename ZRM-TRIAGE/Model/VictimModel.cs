using System;
using System.Collections.Generic;
using System.Text;

namespace ZRM_TRIAGE
{
   public class VictimModel : Component
   {
        public enum TriageColor
        {
            Red,
            Yellow,
            Green,
            Black
        }

        public VictimModel()
        {
            Injuries = new InjuriesModel();
        }

        public string Name { get; set; }
        public string Surname { get; set; }
        public string EventId { get; set; }
        public TriageColor Color { get; set; }
        public Xamarin.Forms.Color TextColor { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public DateTime BirthDate { get; set; }
        public InjuriesModel Injuries { get; set; }
        public string Hospital { get; set; }
        public string HospitalId { get; set; }
        public string Ambulance { get; set; }
        public string AmbulanceId { get; set; }
        public bool IsTransported { get; set; }

    }
}
