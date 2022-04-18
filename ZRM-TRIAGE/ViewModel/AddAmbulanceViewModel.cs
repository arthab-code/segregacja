using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

using Firebase.Database.Query;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Firebase.Database;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ZRM_TRIAGE
{
    internal class AddAmbulanceViewModel
    {
        private AmbulanceRepository _ambulanceRepos;
        private ObservableCollection<AmbulanceModel> _ambulances;

        public AddAmbulanceViewModel()
        {
            _ambulanceRepos = new AmbulanceRepository();
            _ambulances = new ObservableCollection<AmbulanceModel>();
        }

        public bool CheckAmbulanceExists(string ambulanceNumber)
        {
            bool isExists = false;

            /* niebezpieczne miejsce w programie */
            /* var thisSame =  _db.GetClient().Child("Crews").OnceAsync<AmbulanceModel>().Result;

             foreach (var item in thisSame)
             {
                 if (item.Object.Number == ambulanceNumber && item.Object.EventId == UserInfo.EventId && isExists == false)
                 {
                     isExists = true;
                     break;
                 }
             }*/

            AmbulanceModel check = _ambulanceRepos.Search(ambulanceNumber);

            if (check != null)
                isExists = true;

            return isExists;

        }

        public bool CheckAmbulanceFunctionExists(AmbulanceModel.Function function)
        {
            bool isExists = false;

            bool search = _ambulanceRepos.SearchFunction(function);

            if (search == true)
                isExists = true;

            return isExists;
        }

        public void AddAmbulanceToDatabase(AmbulanceModel ambulance)
        {
            _ambulanceRepos.Add(ambulance);
        }

        public AmbulanceModel.Function AmbulanceFunctionAdd(int selectedIndex)
        {
            switch (selectedIndex)
            {
                case 0:
                    return AmbulanceModel.Function.Major;
                    break;

                case 1:
                    return AmbulanceModel.Function.Red;
                    break;

                case 2:
                    return AmbulanceModel.Function.Yellow;
                    break;

                case 3:
                    return AmbulanceModel.Function.Green;
                    break;

                case 4:
                    return AmbulanceModel.Function.Transport;
                    break;

                default:
                    return AmbulanceModel.Function.Transport;
                    break;

            }
        }
    }
}
