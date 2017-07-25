using System;
using Demo.Domain;

namespace Demo.V1
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
            var tariff = salesOrder.Customer.Tariff;
            var invoice = new Invoice(salesOrder.Customer, 
                tariff.OrderCharge, 
                tariff.ShippingCharges[msg.ShippingType]);
            
            Db.Insert(invoice);

            Console.WriteLine("Success!");
        }
    }
}