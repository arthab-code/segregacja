using System;
using System.Collections.Generic;
using System.Text;

namespace ZRM_TRIAGE
{
    public class HospitalModel : Component
    {
        public string Name { get; set; }
        public string City { get; set; }
        public string Street { get; set; }

        public List<VictimModel> VictimList { get; set; }

        public HospitalModel()
        {
            VictimList = new List<VictimModel>();
        }
    }
}
