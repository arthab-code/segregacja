using System;
using System.Collections.Generic;
using System.Text;

namespace ZRM_TRIAGE
{
    internal class HospitalAddViewModel
    {
        private HospitalRepository _hospitalRepository;

        public HospitalAddViewModel()
        {
            _hospitalRepository = new HospitalRepository();  
        }

        public void AddHospital(HospitalModel hospital)
        {
            _hospitalRepository.Add(hospital);
        }
    }
}
