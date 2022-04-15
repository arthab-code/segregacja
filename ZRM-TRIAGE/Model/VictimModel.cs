using System;
using System.Collections.Generic;
using System.Text;

namespace ZRM_TRIAGE
{
   public class VictimModel
   {
        public enum Injuries
        {
            Head,
            Neck,
            Chest,
            Stomach,
            Pelvis,
            Back,
            LeftLeg,
            RightLeg,
            LeftArm,
            RightArm
        }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string EventId { get; set; }
        public string TriageColor { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public DateTime BirthDate { get; set; }
        public Injuries Damages { get; set; }

    }
}
