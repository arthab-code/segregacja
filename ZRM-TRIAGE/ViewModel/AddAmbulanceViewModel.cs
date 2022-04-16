using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

using Firebase.Database.Query;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Firebase.Database;

namespace ZRM_TRIAGE
{
    internal class AddAmbulanceViewModel
    {
        private Database db;
        private ObservableCollection<AmbulanceModel> ambulances;

        public AddAmbulanceViewModel()
        {
            db = new Database();
            ambulances = new ObservableCollection<AmbulanceModel>();
        }

        public bool CheckAmbulanceExists(string ambulanceNumber)
        {
            ObservableCollection<AmbulanceModel> checkAmbulanceExists = new ObservableCollection<AmbulanceModel>();

            bool isExists = false;

            var thisSame =  db.GetClient().Child("Crews").OnceAsync<AmbulanceModel>().Result;

            foreach (var item in thisSame)
            {
                if (item.Object.Number == ambulanceNumber && item.Object.EventId == UserInfo.EventId && isExists == false)
                {
                    isExists = true;
                    break;
                }
            }
            return isExists;

        }

        public async void AddAmbulanceToDatabase(AmbulanceModel ambulance)
        {
            await db.GetClient().Child("Crews").PostAsync(ambulance);
        }


    }
}
