using System;
using Demo.Domain;
using Demo.Rop;
using Demo.V4Rop;

namespace Demo.V4
{
    public class CreateInvoiceWhenSalesOrderShipped : IHandler<OrderShipped>
    {
        public CreateInvoiceWhenSalesOrderShipped(IRopDatabase db)
        {
            this.Db = db;
        }

        private IRopDatabase Db { get; }

        public void Handle(OrderShipped msg)
        {
            HandleInternal(msg)
                .Handle(i => Console.WriteLine("Success!"),
                    err => Console.WriteLine(string.Join("\n", err)));
        }

        public Result<Invoice,string[]> HandleInternal(OrderShipped msg)
        {
            return Db.GetRop<SalesOrder>(msg.OrderId)
                .Bind(SalesOrderValidator.ValidateForInvoicing)
                .Map(so => new Invoice(so.Customer, 
                    so.Customer.Tariff.OrderCharge, 
                    so.Customer.Tariff.ShippingCharges[msg.ShippingType]))
                .Bind(Db.InsertRop);
        }
    }
}