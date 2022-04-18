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
        private AmbulanceRepository _ambulanceRepos;
        private ObservableCollection<AmbulanceModel> _ambulances;

        public AmbulanceListViewModel()
        {
            _ambulanceRepos = new AmbulanceRepository();
            _ambulances = new ObservableCollection<AmbulanceModel>();
        }
        public List<AmbulanceModel> GetAllAmbulances()
        {
            return _ambulanceRepos.GetAll();
        }
    }
}
