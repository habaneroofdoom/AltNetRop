using System;
using System.Collections.Generic;
using Demo.Domain;
using Demo.Rop;

namespace Demo.V4
{
    public interface IRopDatabase : IDatabase
    {
        Result<T, string[]> GetRop<T>(int id) where T: class, IPersisted;
        Result<T, string[]> InsertRop<T>(T obj) where T: class, IPersisted;
    }

    public class RopDatabase : V1.Database, IRopDatabase
    {
        private Random rand = new Random();
        private const int BombFrequency = 3;

        public override T Get<T>(int id)
        {
            if (rand.Next() % BombFrequency == 0)
                return null;

            return base.Get<T>(id);
        }

        public Result<T, string[]> GetRop<T>(int id) where T: class, IPersisted
        {
            var result = Get<T>(id);

            return result != null
                ? Result<T, string[]>.Succeeded(result)
                : Result<T, string[]>.Failed(new [] {$"Could not find {typeof(T).Name} with ID {id}"});
        }

        public override void Insert<T>(T obj)
        {
            if (rand.Next() % BombFrequency == 0)
                throw new RandomDbException();

            base.Insert<T>(obj);
        }

        public Result<T, string[]> InsertRop<T>(T obj) where T: class, IPersisted
        {
            try
            {
                Insert(obj);
                return Result<T, string[]>.Succeeded(obj);
            }
            catch (RandomDbException rex)
            {
                return Result<T, string[]>.Failed(new [] {rex.Message});
            }
        }
    }
}