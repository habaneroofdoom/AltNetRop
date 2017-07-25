using StructureMap;
using Demo.Domain;

namespace Demo.V3
{
    public class V3Registry : Registry
    {
        public V3Registry()
        {
            ForSingletonOf<IDatabase>().Use<V2.FlakyDatabase>();
            For<IHandler<OrderShipped>>().Use<CreateInvoiceWhenSalesOrderShipped>();
        }
    }
}