using System;
using System.Collections.Generic;
using System.Text;

namespace ZRM_TRIAGE
{
    public class HospitalsListViewModel
    {
        private HospitalRepository _hospitalRepository;

        public HospitalsListViewModel()
        {
            _hospitalRepository = new HospitalRepository();   
        }

        public List<HospitalModel> GetAll()
        {
            return _hospitalRepository.GetAll();
        }
    }
}
