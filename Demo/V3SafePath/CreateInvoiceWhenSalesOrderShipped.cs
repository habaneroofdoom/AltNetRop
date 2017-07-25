using System;
using Demo.Domain;

namespace Demo.V3
{
    public class CreateInvoiceWhenSalesOrderShipped : IHandler<OrderShipped>
    {
        public CreateInvoiceWhenSalesOrderShipped(IDatabase db)
        {
            this.Db = db;
        }

        private IDatabase Db { get; }

        public void Handle(OrderShipped msg)
        {
            var salesOrder = Db.Get<SalesOrder>(msg.OrderId);

            if (salesOrder == null)
            {
                Console.WriteLine("Could not find sales order");
                return;
            }

            if (salesOrder.OrderPickDate == null)
            {
                Console.WriteLine("Sales order has no pick date");
                return;
            }

            var tariff = salesOrder.Customer.Tariff;

            if (tariff == null)
            {
                Console.WriteLine("Customer has no tariff");
                return;
            }

            var invoice = new Invoice(salesOrder.Customer, tariff.OrderCharge,
                tariff.ShippingCharges[msg.ShippingType]);

            try
            {
                Db.Insert(invoice);
                Console.WriteLine("Success!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving: {ex.Message}");
            }

        }
    }
}