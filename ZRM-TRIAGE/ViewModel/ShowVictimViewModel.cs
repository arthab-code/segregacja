using System;
using System.Collections.Generic;
using System.Text;

namespace ZRM_TRIAGE
{
    internal class ShowVictimViewModel
    {

        private VictimRepository _victimRepository;

        public ShowVictimViewModel()
        {
            _victimRepository = new VictimRepository();
        }

        public void DeleteVictim(VictimModel victimModel)
        {
            _victimRepository.Remove(victimModel.Id);
        }

        public void UpdateVictim(VictimModel oldVictim, VictimModel newVictim)
        {
            _victimRepository.Update(oldVictim, newVictim);
        }


    }
}
