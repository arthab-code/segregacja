using System;
using System.Collections.Generic;
using System.Text;

namespace ZRM_TRIAGE
{
    internal class SelectVictimsForAmbulanceViewModel
    {
        private VictimRepository _victimRepository;

        public SelectVictimsForAmbulanceViewModel()
        {
            _victimRepository = new VictimRepository();
        }

        public void AddVictim(VictimModel victim)
        {
            _victimRepository.Update(victim, victim);
        }
    }
}
