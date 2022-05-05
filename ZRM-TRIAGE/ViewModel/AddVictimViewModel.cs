using System;
using System.Collections.Generic;
using System.Text;

namespace ZRM_TRIAGE
{
    internal class AddVictimViewModel
    {
        private VictimRepository _victimRepository;

        public AddVictimViewModel()
        {
           _victimRepository = new VictimRepository();
        }

        public void AddVictim(VictimModel victim)
        {
            _victimRepository.Add(victim);
        }

    }
}
