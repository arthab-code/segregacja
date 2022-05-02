using Firebase.Database.Query;
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
            bool isExists = false;

            AmbulanceModel check = _ambulanceRepository.Search(ambulanceNumber);

            if (check != null)
                isExists = true;

            return isExists;
        }

        public void Update(AmbulanceModel oldItem, AmbulanceModel newItem)
        {
            _ambulanceRepository.Update(oldItem, newItem);
        }

        public bool SearchFunction(AmbulanceModel.Function function)
        {
            bool isExists = false;

            if (function == AmbulanceModel.Function.Transport)
                return false;

            var search = _ambulanceRepository.GetDatabase().GetClient().Child("Crews").Child(UserInfo.EventId).OnceAsync<AmbulanceModel>().Result;

            foreach (var item in search)
            {

                if (item.Object.AmbulanceFunction == function)
                {
                    isExists = true;
                    break;
                }
            }

            return isExists;
        }


        public bool CheckAmbulanceFunctionExists(AmbulanceModel.Function function)
        {
            bool isExists = false;

            bool search = SearchFunction(function);

            if (search == true)
                isExists = true;

            return isExists;
        }

        public AmbulanceModel.Function AmbulanceFunctionAdd(int index)
        {
            switch (index)
            {
                case 0:
                    return AmbulanceModel.Function.Major;

                case 1:
                    return AmbulanceModel.Function.Red;

                case 2:
                    return AmbulanceModel.Function.Yellow;

                case 3:
                    return AmbulanceModel.Function.Green;

                case 4:
                    return AmbulanceModel.Function.Transport;

                default:
                    return AmbulanceModel.Function.Transport;
            }
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

        public AmbulanceRepository GetRepo()
        {
            return _ambulanceRepository;
        }
    }
}
