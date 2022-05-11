using System;
using System.Collections.Generic;
using System.Text;

namespace ZRM_TRIAGE
{
    internal class SelectHospitalForAmbulanceViewModel
    {
        private AmbulanceRepository _ambulanceRepository;

        public SelectHospitalForAmbulanceViewModel()
        {
            _ambulanceRepository = new AmbulanceRepository();
        }

        public void SaveHospital(AmbulanceModel ambulance)
        {
            _ambulanceRepository.Update(ambulance, ambulance);
        }
    }
}
