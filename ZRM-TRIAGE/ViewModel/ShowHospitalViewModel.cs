using System;
using System.Collections.Generic;
using System.Text;

namespace ZRM_TRIAGE
{
    internal class ShowHospitalViewModel
    {

        private HospitalRepository _hospitalRepository;

        public ShowHospitalViewModel()
        {
            _hospitalRepository = new HospitalRepository();
        }

        public void DeleteHospital(HospitalModel hospital)
        {
            _hospitalRepository.Remove(hospital.DatabaseId);
        }

        public void UpdateHospital(HospitalModel hospital)
        {
            _hospitalRepository.Update(hospital);
        }

        public bool CheckHospitalExists(string name)
        {
            bool isExists = false;

            HospitalModel check = _hospitalRepository.Search(name);

            if (check != null)
                isExists = true;

            return isExists;
        }

    }
}
