using System;
using System.IO;
using System.Collections.Generic;
using System.Text;

namespace ZRM_TRIAGE
{
    public class FileSystem
    {
        private string _path;
        private string _fileName;
        public FileSystem()
        {
            _fileName = "login.txt";
             _path = Xamarin.Essentials.FileSystem.AppDataDirectory + "/" + _fileName;
        }

        public string GetPath()
        {
            return _path;            
        }

        public bool CheckFileExists()
        {
            return File.Exists(_path);
        }

        public string RetrieveLogin()
        {
            string result = File.ReadAllText(_path);
            var results = result.Split('\n');

            return results[0];
        }

        public string RetrieveEventId()
        {
            string result = File.ReadAllText(_path);
            var results = result.Split('\n');

            return results[1];
        }

        public void DeleteFile()
        {
            if (CheckFileExists())
              File.Delete(_path);
        }

        public void CreateDataFile(string pwd,string eventId)
        {
            string combine = pwd + "\n" + eventId;
           if (!CheckFileExists()) 
             File.WriteAllText(_path, combine);
        }

    }
}
