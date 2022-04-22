using System;
using System.Collections.Generic;
using System.Text;

namespace ZRM_TRIAGE
{
    public class TriagePageViewModel
    {
        private TriageRepository _triageRepos;

        public TriagePageViewModel()
        {
            _triageRepos = new TriageRepository();
        }

        public void GetTriageModel()
        {
            _triageRepos.GetTriageModel();
        }

        public void SaveTriageModel()
        {
            _triageRepos.SaveTriageModel();
        }

        public void NumberRed(int value)
        {
            _triageRepos.NumberRed(value);
        }

        public void NumberYellow(int value)
        {
            _triageRepos.NumberYellow(value);
        }

        public void NumberGreen(int value)
        {
            _triageRepos.NumberGreen(value);
        }

        public void NumberBlack(int value)
        {
            _triageRepos.NumberBlack(value);
        }

        public TriageModel GetTriageModelObject() => _triageRepos.GetTriageModelObject();

    }
}
