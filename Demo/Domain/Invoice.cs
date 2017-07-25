using System;

namespace Demo.Domain
{
    public class Invoice : IPersisted
    {

        public Invoice(Customer customer, decimal charge, decimal shipping)
        {
            Customer = customer ?? throw new ArgumentNullException(nameof(customer));
            Charge = charge;
            Shipping = shipping;
            Total = charge + shipping;
        }


        public int Id { get; set; }
        public Customer Customer { get; private set; }
        public decimal Charge { get; private set; }
        public decimal Shipping { get; private set; }
        public decimal Total { get; private set; }
    }
}