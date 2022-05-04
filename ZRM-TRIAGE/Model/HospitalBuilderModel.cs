using System;
using System.Collections.Generic;
using System.Text;

namespace ZRM_TRIAGE
{
    internal class HospitalBuilderModel
    {
        private HospitalModel _hospitalModel;
        private ShowHospitalViewModel _showHospitalViewModel;
        public bool IsCreate { get; private set; }
        public string CreateText { get; private set; }

        public HospitalBuilderModel()
        {
            _hospitalModel = new HospitalModel();
            _showHospitalViewModel = new ShowHospitalViewModel();
            IsCreate = true;
        }

        public HospitalBuilderModel HospitalNameSet(string name)
        {
            if (IsCreate == false) return this;

            if (_showHospitalViewModel.CheckHospitalExists(name))
            {
                IsCreate = false;
                CreateText = "Taki szpital już istnieje!";

                return this;
            }

            if (name == null || name == "")
            {
                IsCreate = false;
                CreateText = "Nazwa szpitala nie mooże być pusta";

                return this;
            }

            _hospitalModel.Name = name;

            return this;
        }

        public HospitalBuilderModel HospitalCitySet(string name)
        {
            if (IsCreate == false) return this;

            if (name == null || name == "")
            {
                IsCreate = false;
                CreateText = "Musisz uzupełnić nazwę miasta gdzie znajduje się szpital";

                return this;
            }

            _hospitalModel.City = name;

            return this;

        }

        public HospitalBuilderModel HospitalStreetSet(string name)
        {
            if (IsCreate == false) return this;

            if (name == null || name == "")
            {
                IsCreate = false;
                CreateText = "Musisz uzupełnić nazwę ulicy gdzie znajduje się szpital";

                return this;
            }

            _hospitalModel.Street = name;

            return this;

        }

        public HospitalModel Build()
        {
            return _hospitalModel;
        }

    }
}
