using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Firebase;
using Firebase.Database;
using Firebase.Database.Query;

namespace ZRM_TRIAGE
{
    public class Database<T> : IDatabase<T> where T : class
    {
        private string _connectionString = "https://zrm-triage-alpha-default-rtdb.europe-west1.firebasedatabase.app/";
        private FirebaseClient _fbClient;


        public bool IsConnected { get; private set; }

        public Database()
        {
            
            IsConnected = true;
            

            try
            {
                _fbClient = new FirebaseClient(_connectionString);

            } catch (Exception e)
            {
                DatabaseError.Show("1");
            }
        }

        public void Create(string dbName, string eventId, T record)
        {
            try
            {
                var obj = GetClient().Child(dbName).Child(eventId).PostAsync(record).Result;

                var recordKey = obj.Key;

                var componentObject = record as Component;
                componentObject.DatabaseId = recordKey;

                GetClient().Child(dbName).Child(eventId).Child(recordKey).PutAsync(componentObject);

            }catch(Exception e)
            {
                DatabaseError.Show("2");
            }
        }

        public void Create(string dbName,  T record)
        {
            try
            {
                var obj = GetClient().Child(dbName).PostAsync(record).Result;

                var recordKey = obj.Key;

                var componentObject = record as Component;
                componentObject.DatabaseId = recordKey;

                GetClient().Child(dbName).Child(recordKey).PutAsync(componentObject);

            }catch(Exception e)
            {
                DatabaseError.Show("3");
            }
        }

        public void Delete(string dbName, string eventId, string recordKey)
        {
            try
            {
                GetClient().Child(dbName).Child(eventId).Child(recordKey).DeleteAsync();
            }catch (Exception e)
            {
                DatabaseError.Show("4");
            }
        }

        public void Delete(string dbName, string eventId)
        {
            try
            {
                GetClient().Child(dbName).Child(eventId).DeleteAsync();
            }
            catch (Exception e)
            {
                DatabaseError.Show("5");
            }
        }

        public T Read(string dbName, string eventId, string recordKey)
        {
            try
            {
                var tmp = GetClient().Child(dbName).Child(eventId).Child(recordKey).OnceSingleAsync<T>().Result;

                return tmp;

            } catch (Exception e)
            {
                DatabaseError.Show("6");
                return null;
            }
        }

        public T Read(string dbName, string eventId)
        {
            try
            {
                var tmp = GetClient().Child(dbName).Child(eventId).OnceAsync<T>().Result.FirstOrDefault();

                return tmp.Object;

            }catch (Exception e)
            {
                DatabaseError.Show("7");
                return null;
            }
        }

        public T Read(string dbName)
        {
            try
            {
                var tmp = GetClient().Child(dbName).OnceAsync<T>().Result.FirstOrDefault();

                return tmp?.Object;
            } catch (Exception e)
            {
                DatabaseError.Show("8");
                return null;
            }
        }


        public List<T> ReadAll(string dbName, string eventId)
        {
            try
            {
                List<T> values = new List<T>();

                var tmp = GetClient().Child(dbName).Child(eventId).OnceAsync<T>().Result.ToList();

                foreach (var item in tmp)
                    values.Add(item.Object);

                return values;

            } catch (Exception e)
            {
                DatabaseError.Show("9");
                return new List<T>();
            }
        }

        public List<T> ReadAll(string dbName)
        {
            try
            {
                List<T> values = new List<T>();

                var tmp = GetClient().Child(dbName).OnceAsync<T>().Result.ToList();

                foreach (var item in tmp)
                    values.Add(item.Object);

                return values;

            }
            catch (Exception e)
            {
                DatabaseError.Show("10");
                return new List<T>();
            }
        }

        public void Update(string dbName, string eventId, string recordKey, T record)
        {
            try
            {
                GetClient().Child(dbName).Child(eventId).Child(recordKey).PutAsync(record);
            }catch(Exception e)
            {
                DatabaseError.Show("11");
            }
        }

        public FirebaseClient GetClient() => _fbClient;

    }
}
