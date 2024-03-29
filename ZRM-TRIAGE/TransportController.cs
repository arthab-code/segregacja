﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace ZRM_TRIAGE
{
    public class TransportController
    {
        private AmbulanceRepository _ambulanceRepository;
        private HospitalRepository _hospitalRepository;
        private VictimRepository _victimRepository;

        public TransportController()
        {
            _ambulanceRepository = new AmbulanceRepository();
            _hospitalRepository = new HospitalRepository();
            _victimRepository = new VictimRepository();
        }

        public List<AmbulanceModel> GetAmbulances()
        {
            return _ambulanceRepository
                .GetAll()
                .Where<AmbulanceModel>(a => a.AmbulanceFunction == AmbulanceModel.Function.Transport && a.AmbulanceStatus == AmbulanceModel.Status.AtLocation)
                .ToList<AmbulanceModel>();
        }

        public List<HospitalModel> GetHospitals()
        {
            return _hospitalRepository.GetAll();

        }

        public List<VictimModel> GetVictims()
        {
            return _victimRepository.GetAll();
        }

        public void SaveVictimData(VictimModel victim)
        {
            _victimRepository.Update(victim);
        }

        public void SaveData(VictimModel victim, AmbulanceModel ambulance, HospitalModel hospital)
        {
            _victimRepository.Update(victim);
            _ambulanceRepository.Update(ambulance);
            _hospitalRepository.Update(hospital);
        }

        public void CorrectData(VictimModel victim)
        {
            if (victim.AmbulanceId == null || victim.HospitalId == null)
                return;

            AmbulanceModel ambulanceCheck = _ambulanceRepository.GetAmbulance(victim.AmbulanceId);
            HospitalModel hospitalCheck = _hospitalRepository.Search(victim.HospitalId);

            /** DELETE VICTIM FROM VICTIM LIST IN AMBULANCE MODEL **/
            for (int i=0; i< ambulanceCheck.Victims.Count; i++)
            {
                if (ambulanceCheck.Victims[i].AmbulanceId == victim.AmbulanceId)
                {
                    ambulanceCheck.Victims.RemoveAt(i);

                }
            }

            /** DELETE VICTIM FROM VICTIM LIST IN HOSPITAL MODEL **/
            for (int i = 0; i < hospitalCheck.VictimList.Count; i++)
            {
                if (hospitalCheck.VictimList[i].HospitalId == victim.HospitalId)
                {
                    hospitalCheck.VictimList.RemoveAt(i);

                }
            }

            _ambulanceRepository.Update(ambulanceCheck);
            _hospitalRepository.Update(hospitalCheck);

        }
    }
}
