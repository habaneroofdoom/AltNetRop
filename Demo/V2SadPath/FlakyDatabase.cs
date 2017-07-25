using System;
using System.Collections.Generic;
using Demo.Domain;

namespace Demo.V2
{
    public class FlakyDatabase : V1.Database
    {
        private Random rand = new Random();
        private const int BombFrequency = 3;

        public override T Get<T>(int id)
        {
            if (rand.Next() % BombFrequency == 0)
                return null;

            return base.Get<T>(id);
        }

        public override void Insert<T>(T obj)
        {
            if (rand.Next() % BombFrequency == 0)
                throw new RandomDbException();

            base.Insert<T>(obj);
        }
    }
}