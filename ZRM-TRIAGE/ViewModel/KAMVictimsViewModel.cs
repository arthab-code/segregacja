using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace ZRM_TRIAGE
{
    internal class KAMVictimsViewModel
    {
        private VictimRepository _victimRepository;

        public KAMVictimsViewModel()
        {
            _victimRepository = new VictimRepository();
        }

        public List<VictimModel> GetVictimList(VictimModel.TriageColor triageColor)
        {
            return _victimRepository.GetAll().Where(a => a.Color == triageColor).ToList<VictimModel>();
        }



    }
}
