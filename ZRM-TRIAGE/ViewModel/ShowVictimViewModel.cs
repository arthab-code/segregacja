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
            _victimRepository.Remove(victimModel.DatabaseId);
        }

        public void UpdateVictim(VictimModel victim)
        {
            _victimRepository.Update(victim);
        }

        public TransportController GetTransportController()
        {
            return _transportController;
        }



    }
}
