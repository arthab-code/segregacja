using System;
using System.Collections.Generic;
using System.Text;

namespace ZRM_TRIAGE
{
    internal class InjuriesCreatorModel
    {
        private InjuriesModel _injuriesModel;

        public InjuriesCreatorModel(InjuriesModel injuriesModel)
        {
            _injuriesModel = injuriesModel;
        }

        public void Head(bool value)
        {
            _injuriesModel.Head = value;
        }

        public void Neck(bool value)
        {
            _injuriesModel.Neck = value;
        }

        public void SpineNeck(bool value)
        {
            _injuriesModel.NeckSpine = value;
        }

        public void Chest(bool value)
        {
            _injuriesModel.Chest = value;
        }

        public void Back(bool value)
        {
            _injuriesModel.Back = value;
        }

        public void Spine(bool value)
        {
            _injuriesModel.Spine = value;
        }

        public void Stomach(bool value)
        {
            _injuriesModel.Stomach = value;
        }

        public void Pelvis(bool value)
        {
            _injuriesModel.Pelvis = value;
        }

        public void LeftLeg(bool value)
        {
            _injuriesModel.LeftLeg = value;
        }

        public void RightLeg(bool value)
        {
            _injuriesModel.RightLeg = value;    
        }

        public void LeftArm(bool value)
        {
            _injuriesModel.LeftArm = value;
        }

        public void RightArm(bool value)
        {
            _injuriesModel.RightArm = value;
        }

        public void Burn(bool value, int percent)
        {
            _injuriesModel.Burn = value;
            _injuriesModel.PercentBurn = percent;
        }

        public void Frostbite(bool value, int percent)
        {
            _injuriesModel.Frostbite = value;
            _injuriesModel.PercentFrostbite = percent;
        }

    }
}
