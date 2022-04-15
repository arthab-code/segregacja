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
    internal class AmbulanceListViewModel
    {
        private Database db;
        private ObservableCollection<AmbulanceModel> ambulances;

        public AmbulanceListViewModel()
        {
            db = new Database();
            ambulances = new ObservableCollection<AmbulanceModel>();
        }

        public async Task<List<AmbulanceModel>> GetAllAmbulances()
        {
            return (await db.GetClient().
                Child("Crews")
                .OnceAsync<AmbulanceModel>())
                .Select(item => new AmbulanceModel
                {

                    Number = item.Object.Number

                })
                .Where(item => item.Number == "12")
                .ToList();
        }


    }
}
