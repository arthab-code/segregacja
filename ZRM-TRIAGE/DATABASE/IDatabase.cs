using System;
using System.Collections.Generic;
using System.Text;

namespace ZRM_TRIAGE
{
    public interface IDatabase<T>
    {

        void Create(string dbName, string eventId, T record);

        void Create(string dbName, T record);

        void Update(string dbName, string eventId, string recordKey, T record);

        T Read(string dbName, string eventId, string recordKey);

        T Read(string dbName, string eventId);

        T Read(string dbName);

        List<T> ReadAll(string dbName, string eventId);

        void Delete(string dbName, string eventId, string recordKey);

        void Delete(string dbName, string eventId);


    }
}
