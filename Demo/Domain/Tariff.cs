using System.Collections.Generic;

namespace Demo.Domain
{
    public class Tariff : IPersisted
    {
        public Tariff(decimal orderCharge, IDictionary<ShippingType, decimal> shippingCharges)
        {
            OrderCharge = orderCharge;
            ShippingCharges = shippingCharges;
        }

        public int Id { get; set; }
        public decimal OrderCharge { get; private set; }
        public IDictionary<ShippingType, decimal> ShippingCharges { get; private set; }
    }
}