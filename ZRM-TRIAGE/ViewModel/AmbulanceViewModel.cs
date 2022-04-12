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
    public class AmbulanceViewModel
    {
        public ObservableCollection<AmbulanceModel> AmbulanceList { get; set; }

        public AmbulanceViewModel()
        {
            AmbulanceList = new ObservableCollection<AmbulanceModel>();
        }

    }
}
