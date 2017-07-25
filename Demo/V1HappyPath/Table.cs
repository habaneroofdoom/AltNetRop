using System.Collections.Generic;
using Demo.Domain;

namespace Demo.V1
{
    public class Table<T> where T : IPersisted
    {
        private static IList<T> Data = new List<T>();

        public T Get(int id)
        {
            return Data[id - 1];
        }

        public void Insert(T obj)
        {
            Data.Add((T)obj);
            obj.Id = Data.Count;
        }
    }
}