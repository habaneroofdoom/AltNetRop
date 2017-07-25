using System;
using System.Collections.Generic;
using Demo.Domain;

namespace Demo.V1
{
    public class Database : IDatabase
    {
        public Database()
        {
            Initialize();
        }

        protected static Table<SalesOrder> SalesOrders => new Table<SalesOrder>();
        protected static Table<Invoice> Invoices => new Table<Invoice>();

        protected IDictionary<Type, Func<int, IPersisted>> GetFuncs
            = new Dictionary<Type, Func<int, IPersisted>>
            {
                [typeof(SalesOrder)] = SalesOrders.Get,
                [typeof(Invoice)] = Invoices.Get
            };

        protected IDictionary<Type, Action<IPersisted>> InsertFuncs
            = new Dictionary<Type, Action<IPersisted>>
            {
                [typeof(SalesOrder)] = o => SalesOrders.Insert((SalesOrder)o),
                [typeof(Invoice)] = o => Invoices.Insert((Invoice)o)
            };

        public virtual T Get<T>(int id) where T : class, IPersisted
        {
            // Console.WriteLine($"Getting {typeof(T).Name} with Id {id}");
            return (T)GetFuncs[typeof(T)](id);
        }
        public virtual void Insert<T>(T obj) where T : class, IPersisted
        {
            InsertFuncs[typeof(T)](obj);
            // Console.WriteLine($"Saved {typeof(T).Name} with Id {obj.Id}");
        }

        public virtual void Initialize()
        {
            var tariff = new Tariff(100.0m,
                new Dictionary<ShippingType, decimal>
                {
                    [ShippingType.Standard] = 10,
                    [ShippingType.Express] = 50,
                    [ShippingType.Teleport] = 500
                });

            SalesOrders.Insert(new SalesOrder("SO123", DateTime.Today, new Customer("Bob", tariff)));
            SalesOrders.Insert(new SalesOrder("SO456", null, new Customer("Mary", null)));
        }

    }
}