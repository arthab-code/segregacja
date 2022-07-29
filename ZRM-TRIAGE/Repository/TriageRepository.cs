using Firebase.Database.Query;
using Firebase.Database;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace ZRM_TRIAGE
{
    public class TriageRepository
    {
        private Database<TriageModel> _database;
        private string _dataName = "Triage";
        private string _resultId;

        public TriageRepository()
        {
            _database = new Database<TriageModel>();
        }

        public void InitializeDB()
        {
            //  _database.GetClient().Child(_dataName).Child(UserInfo.EventId).PostAsync(TriageModel.Instance);
            _database.Create(_dataName, UserInfo.EventId, TriageModel.Instance);
        }

        public void GetTriageModel()
        {
            //var result = _database.GetClient().Child(_dataName).Child(UserInfo.EventId).OnceAsync<TriageModel>().Result.FirstOrDefault();
            var result = _database.Read(_dataName, UserInfo.EventId);
            _resultId = result.DatabaseId;
            TriageModel.Instance = result;
        }

        public void LoadTriageData(ref Label red, ref Label yellow, ref Label green, ref Label black )
        {
            GetTriageModel();
            red.Text = TriageModel.Instance.Red.ToString();
            yellow.Text = TriageModel.Instance.Yellow.ToString();
            green.Text = TriageModel.Instance.Green.ToString();
            black.Text = TriageModel.Instance.Black.ToString();
        }

        public void SaveTriageModel()
        {
            //_database.GetClient().Child(_dataName).Child(UserInfo.EventId).Child(_resultId).PutAsync(TriageModel.Instance);
            _database.Update(_dataName, UserInfo.EventId, _resultId, TriageModel.Instance);
        }

        public void NumberRed(int value)
        {
            TriageModel.Instance.Red += value;
            TriageModel.Instance.Amount += value;
            SaveTriageModel();
            GetTriageModel();
        }

        public void NumberYellow(int value)
        {
            TriageModel.Instance.Yellow += value;
            TriageModel.Instance.Amount += value;
            SaveTriageModel();
            GetTriageModel();
        }

        public void NumberGreen(int value)
        {
            TriageModel.Instance.Green += value;
            TriageModel.Instance.Amount += value;
            SaveTriageModel();
            GetTriageModel();
        }

        public void NumberBlack(int value)
        {
            TriageModel.Instance.Black += value;
            TriageModel.Instance.Amount += value;
            SaveTriageModel();
            GetTriageModel();
        }
    }
}
