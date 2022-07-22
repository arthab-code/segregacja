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
        private string _victimDbName;
        private string _hospitalDbName;

        public TransportAmbulanceViewModel()
        {
            _hospitalRepository = new HospitalRepository();
            _ambulanceRepository = new AmbulanceRepository();
            _victimRepository = new VictimRepository();

            _victimDbName = _victimRepository._dataName;
            _hospitalDbName = _hospitalRepository._dataName;
        }

        public void ChangeAmbulanceStatus(AmbulanceModel ambulance, AmbulanceModel.Status status)
        {
            ambulance.AmbulanceStatus = status;

            _ambulanceRepository.Update(ambulance, ambulance);
        }

        public ObservableCollection<VictimModel> GetVictims()
        {
            Database db = new Database();
            ObservableCollection<VictimModel> victims = new ObservableCollection<VictimModel>();

            db.GetClient().Child(_victimDbName).Child(UserInfo.EventId).AsObservable<VictimModel>().Subscribe(a =>
            {
                if (a.Object.Ambulance == UserInfo.Ambulance.Number)
                {
                    if (victims.Contains(a.Object))
                        victims[victims.IndexOf(a.Object)] = a.Object;
                    else
                        victims.Add(a.Object);
                } else
                {
                    victims.Remove(a.Object);
                }
            });

            return victims;
        }

        public ObservableCollection<HospitalModel> GetHospital()
        {
            Database db = new Database();
            ObservableCollection<HospitalModel> hospitals = new ObservableCollection<HospitalModel>();

            db.GetClient().Child(_hospitalDbName).Child(UserInfo.EventId).AsObservable<HospitalModel>().Subscribe(a =>
            {
                if (a.Object.Id == UserInfo.Ambulance.HospitalId)
                {
                    if (hospitals.Contains(a.Object))
                        hospitals[hospitals.IndexOf(a.Object)] = a.Object;
                    else
                        hospitals.Add(a.Object);
                }
                else
                {
                    hospitals.Remove(a.Object);
                }
            });

            return hospitals;
        }

    }
}
