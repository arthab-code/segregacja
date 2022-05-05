using System;
using System.Collections.Generic;
using System.Text;

namespace ZRM_TRIAGE
{
   public class VictimModel
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

        public string Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string EventId { get; set; }
        public TriageColor Color { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public DateTime BirthDate { get; set; }
        public InjuriesModel Injuries { get; set; }

    }
}
