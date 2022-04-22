using System;
using System.Collections.Generic;
using System.Text;

namespace ZRM_TRIAGE
{
    public class ShowAmbulanceViewModel
    {

        private AmbulanceRepository _ambulanceRepository;

        public ShowAmbulanceViewModel()
        {
            _ambulanceRepository = new AmbulanceRepository();  
        }

        public bool CheckAmbulanceExists(string ambulanceNumber)
        {
            return _ambulanceRepository.CheckAmbulanceExists(ambulanceNumber);
        }

        public bool CheckAmbulanceFunctionExists(AmbulanceModel.Function function)
        {
            return _ambulanceRepository.CheckAmbulanceFunctionExists(function);
        }

        public AmbulanceModel.Function AmbulanceFunctionAdd(int index)
        {
            return _ambulanceRepository.AmbulanceFunctionAdd(index);
        }

        public int IndexPicker(int index)
        {
            switch (index)
            {
                case 0:
                    return 0;

                case 1:
                    return 1;

                case 2:
                    return 2;

                case 3:
                    return 3;

                case 4:
                    return 4;

                default:
                    return 4;
            }

        }
    }
}
