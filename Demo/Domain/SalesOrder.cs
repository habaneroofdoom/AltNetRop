using System;

namespace Demo.Domain
{
    public class SalesOrder : IPersisted
    {
        public SalesOrder(string orderNumber, DateTime? orderPickDate, Customer customer)
        {
            OrderNumber = orderNumber;
            OrderPickDate = orderPickDate;
            Customer = customer;
        }

        public int Id { get; set; }
        public string OrderNumber { get; private set; }
        public DateTime? OrderPickDate {get; private set;}
        public Customer Customer { get; private set; }
    }

    public class Customer
    {
        public Customer(string name, Tariff tariff)
        {
            Name = name;
            Tariff = tariff;
        }

        public string Name { get; private set; }
        public Tariff Tariff { get; private set; }
    }
}