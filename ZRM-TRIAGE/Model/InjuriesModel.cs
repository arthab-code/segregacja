using System;
using System.Collections.Generic;
using System.Text;

namespace ZRM_TRIAGE
{
    public class InjuriesModel
    {
        public bool Head { get; set; }
        public bool Neck { get; set; }
        public bool NeckSpine { get; set; }
        public bool Chest { get; set; }
        public bool Stomach { get; set; }
        public bool Pelvis { get; set; }
        public bool Back { get; set; }
        public bool Spine { get; set; }
        public bool LeftLeg { get; set; }
        public bool RightLeg { get; set; }
        public bool LeftArm { get; set; }
        public bool RightArm { get; set; }
        public bool Frostbite { get; set; }
        public int PercentFrostbite { get; set; }
        public bool Burn { get; set; }
        public int PercentBurn { get; set; }
    }
}
