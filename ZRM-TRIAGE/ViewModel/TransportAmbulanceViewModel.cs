using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using Firebase.Database.Query;

namespace ZRM_TRIAGE
{
    public class TransportAmbulanceViewModel
    {
        private HospitalRepository _hospitalRepository;
        private AmbulanceRepository _ambulanceRepository;
        private VictimRepository _victimRepository;
        private ObservableCollection<VictimModel> _victims;
        private ObservableCollection<HospitalModel> _hospitals;

        private string _victimDbName;
        private string _hospitalDbName;
        private string _ambulanceDbName;

        public TransportAmbulanceViewModel()
        {
            _hospitalRepository = new HospitalRepository();
            _ambulanceRepository = new AmbulanceRepository();
            _victimRepository = new VictimRepository();
            _victims = new ObservableCollection<VictimModel>();
            _hospitals = new ObservableCollection<HospitalModel>();
            _victimDbName = _victimRepository._dataName;
            _hospitalDbName = _hospitalRepository._dataName;
            _ambulanceDbName = _ambulanceRepository._dataName;
        }

        public void ChangeAmbulanceStatus(AmbulanceModel ambulance, AmbulanceModel.Status status)
        {
            ambulance.AmbulanceStatus = status;

            _ambulanceRepository.Update(ambulance);
        }

        public ObservableCollection<VictimModel> GetVictims()
        {
            Database<VictimModel> db = new Database<VictimModel>();
            Database<AmbulanceModel> dbAM = new Database<AmbulanceModel>();

            db.GetClient().Child(_victimDbName).Child(UserInfo.EventId).AsObservable<VictimModel>().Subscribe( a =>
            {
                _victims.Clear();

                var tmpList = db.ReadAll(_victimDbName, UserInfo.EventId);

                foreach(var item in tmpList)
                {
                    if (item.AmbulanceId == UserInfo.Ambulance.DatabaseId)
                        _victims.Add(item);

                }

            }); 

            return _victims;
        }

        
        public ObservableCollection<HospitalModel> GetHospital()
        {
            Database<AmbulanceModel> db = new Database<AmbulanceModel>();
            Database<HospitalModel> dbHM = new Database<HospitalModel>();            

            db.GetClient().Child(_ambulanceDbName).Child(UserInfo.EventId).AsObservable<AmbulanceModel>().Subscribe(a =>
            {
                _hospitals.Clear();

                var ambulance = db.Read(_ambulanceDbName, UserInfo.EventId, UserInfo.Ambulance.DatabaseId);
                var hospitals = dbHM.ReadAll(_hospitalDbName, UserInfo.EventId);

                foreach (var item in hospitals)
                {
                    if (item.DatabaseId == ambulance.HospitalId)
                        _hospitals.Add(item);
                }

            });

            return _hospitals;
        }


    }
}
