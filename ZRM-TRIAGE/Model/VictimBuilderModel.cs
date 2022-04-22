using System;
using System.Collections.Generic;
using System.Text;

namespace ZRM_TRIAGE
{
    public class VictimBuilderModel
    {
        private VictimModel _victimModel;

        public VictimBuilderModel()
        {
            _victimModel = new VictimModel();
        }

        public VictimBuilderModel SetVictimName(string name)
        {
            _victimModel.Name = name;

            return this;
        }

        public VictimBuilderModel SetVictimSurname(string surname)
        {
            _victimModel.Surname = surname;

            return this;
        }

        public VictimBuilderModel SetVictimTriageColor(VictimModel.TriageColor triageColor)
        {
            _victimModel.Color = triageColor;

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

        public VictimModel Build() => _victimModel;
    }
}
