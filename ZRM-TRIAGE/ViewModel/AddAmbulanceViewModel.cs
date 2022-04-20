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
            return _ambulanceRepos.CheckAmbulanceExists(ambulanceNumber);
        }

        public bool CheckAmbulanceFunctionExists(AmbulanceModel.Function function)
        {
            return _ambulanceRepos.CheckAmbulanceFunctionExists(function);
        }

        public void AddAmbulanceToDatabase(AmbulanceModel ambulance)
        {
            _ambulanceRepos.Add(ambulance);
        }

        public AmbulanceModel.Function AmbulanceFunctionAdd(int selectedIndex)
        {
            return _ambulanceRepos.AmbulanceFunctionAdd(selectedIndex);            
        }
    }
}
