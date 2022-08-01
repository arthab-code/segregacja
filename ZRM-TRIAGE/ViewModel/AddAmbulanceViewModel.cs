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
        private ShowAmbulanceViewModel _ambulanceViewModel;
        private ObservableCollection<AmbulanceModel> _ambulances;

        public AddAmbulanceViewModel()
        {
            _ambulanceViewModel = new ShowAmbulanceViewModel();
            _ambulances = new ObservableCollection<AmbulanceModel>();
        }

        public bool CheckAmbulanceExists(string ambulanceNumber)
        {
            return _ambulanceViewModel.CheckAmbulanceExists(ambulanceNumber);
        }

        public bool CheckChiefPersonalData(string name, string surname)
        {
            if (name == null || name == " " || surname == null || surname == " ")
                return false;
            else
                return true;
        }

        public bool CheckAmbulanceFunctionExists(AmbulanceModel.Function function)
        {
            return _ambulanceViewModel.CheckAmbulanceFunctionExists(function);
        }

        public void AddAmbulanceToDatabase(AmbulanceModel ambulance)
        {
            _ambulanceViewModel.GetRepo().Add(ambulance);
        }

        public AmbulanceModel.Function AmbulanceFunctionAdd(int selectedIndex)
        {
            return _ambulanceViewModel.AmbulanceFunctionAdd(selectedIndex);            
        }
    }
}
