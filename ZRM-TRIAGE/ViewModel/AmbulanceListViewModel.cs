using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Collections.ObjectModel;

namespace ZRM_TRIAGE
{
    public class AmbulanceListViewModel
    {
        private Database _db;
        private ObservableCollection<AmbulanceModel> _ambulances;

        public AmbulanceListViewModel()
        {
            _db = new Database();
            _ambulances = new ObservableCollection<AmbulanceModel>();
        }
        public async Task<List<AmbulanceModel>> GetAllAmbulances()
        {
            _ambulances.Clear();

            return (await _db.GetClient().
                Child("Crews")
                .OnceAsync<AmbulanceModel>())
                .Select(item => new AmbulanceModel
                {

                    Number = item.Object.Number,
                    EventId = item.Object.EventId

                })
                .Where(item => item.EventId == UserInfo.EventId)
                .ToList();
        }
    }
}
