using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ZRM_TRIAGE
{
    public interface IClientRepository<T>
    {
        void Add(T item);

        void Remove(string item);

        void Update(T item);   

        T Search(string item);
        string SearchKey(string item);

        List<T> GetAll();

    }
}
