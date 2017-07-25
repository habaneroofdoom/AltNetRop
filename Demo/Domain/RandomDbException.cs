using System;

namespace Demo.Domain
{
    public class RandomDbException : Exception
    {
        public RandomDbException(): base("Something went wrong with the DB")
        {

        }
    }
}