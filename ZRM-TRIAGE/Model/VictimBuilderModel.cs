using System;
using System.Collections.Generic;
using System.Text;

namespace ZRM_TRIAGE
{
    public class VictimBuilderModel
    {
        private VictimModel _victimModel;

        public bool IsCreated { get; private set; }
        public string CreateText { get; private set; }

        public VictimBuilderModel()
        {
            _victimModel = new VictimModel();
            IsCreated = true;
        }

        public VictimBuilderModel SetVictimName(string name)
        {
            if (IsCreated == false) return this;

            if (name == null || name.Length == 0 || name == " ")
            {
                CreateText = "Uzupełnij imię";
                IsCreated = false;
            }

            _victimModel.Name = name;

            return this;
        }

        public VictimBuilderModel SetVictimSurname(string surname)
        {
            if (IsCreated == false) return this;

            if (surname == null || surname.Length == 0 || surname == " ")
            {
                CreateText = "Uzupełnij nazwisko";
                IsCreated = false;
            }

            _victimModel.Surname = surname;

            return this;
        }

        public VictimBuilderModel SetVictimTriageColor(int triageColor)
        {
            if (IsCreated == false) return this;

            if (triageColor != 0 && triageColor != 1 && triageColor != 2 && triageColor != 3)
            {
                CreateText = "Uzupełnij kolor triage";
                IsCreated = false;
                return this;
            }

            if (triageColor == 0)
                _victimModel.TextColor = "#D98A8A";
            if (triageColor == 1)
                _victimModel.TextColor = "#D9CB8A";
            if (triageColor == 2)
                _victimModel.TextColor = "#ACD98A";
            if (triageColor == 3)
                _victimModel.TextColor = "#626262";

            _victimModel.Color = (VictimModel.TriageColor)triageColor;

            return this;
        }

        public VictimBuilderModel SetVictimCity(string city)
        {
            _victimModel.City = city;

            return this;
        }

        public VictimBuilderModel SetVictimStreet(string street)
        {
            _victimModel.Street = street;

            return this;
        }

        public VictimBuilderModel SetVictimBirthDate(DateTime birthDate)
        {
            _victimModel.BirthDate = birthDate;

            return this;
        }

        public VictimBuilderModel SetVictimInjuries(InjuriesModel injuries)
        {
            _victimModel.Injuries = injuries;

            return this;
        }

        public VictimBuilderModel SetIsTransported(bool value)
        {
            _victimModel.IsTransported = false;

            return this;
        }

        public VictimModel Build() => _victimModel;
    }
}
