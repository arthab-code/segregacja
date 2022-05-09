using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace ZRM_TRIAGE
{
    internal class ShowVictimViewModel
    {

        private VictimRepository _victimRepository;
        private TransportController _transportController;

        public ShowVictimViewModel()
        {
            _victimRepository = new VictimRepository();
            _transportController = new TransportController();

        }

        public void DeleteVictim(VictimModel victimModel)
        {
            _victimRepository.Remove(victimModel.Id);
        }

        public void UpdateVictim(VictimModel oldVictim, VictimModel newVictim)
        {
            _victimRepository.Update(oldVictim, newVictim);
        }

        public TransportController GetTransportController()
        {
            return _transportController;
        }



    }
}
