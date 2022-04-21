using Firebase.Database.Query;
using Firebase.Database;
using System;
using System.Collections.Generic;
using System.Text;

namespace ZRM_TRIAGE
{
    public class TriageRepository
    {
        private Database _database;
        private TriageModel _triageModel;

        public TriageRepository()
        {
            _database = new Database();
        }

        public void InitializeDB()
        {
            TriageModel triageModel = new TriageModel()
            {
                Red = 0,
                Yellow = 0,
                Green = 0,
                Black = 0,
                Amount = 0
            };

            _database.GetClient().Child("Triage").Child(UserInfo.EventId).PostAsync(triageModel);
        }

        public void GetTriageModel()
        {
            var result = _database.GetClient().Child("Triage").Child(UserInfo.EventId).OnceSingleAsync<TriageModel>().Result;

            _triageModel = result;
        }

        public void SaveTriageModel()
        {
            _database.GetClient().Child("Triage").Child(UserInfo.EventId).PutAsync(_triageModel);
        }

        public void NumberRed(int value)
        {
            GetTriageModel();
            _triageModel.Red += value;
            SaveTriageModel();
        }

        public void NumberYellow(int value)
        {
            GetTriageModel();
            _triageModel.Yellow += value;
            SaveTriageModel();
        }

        public void NumberGreen(int value)
        {
            GetTriageModel();
            _triageModel.Green += value;
            SaveTriageModel();
        }

        public void NumberBlack(int value)
        {
            GetTriageModel();
            _triageModel.Black += value;
            SaveTriageModel();
        }

        public TriageModel GetTriageModelObject() => _triageModel;
    }
}
