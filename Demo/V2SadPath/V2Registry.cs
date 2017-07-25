using StructureMap;
using Demo.Domain;
using Demo.V1;

namespace Demo.V2
{
    public class V2Registry : Registry
    {
        public V2Registry()
        {
            ForSingletonOf<IDatabase>().Use<FlakyDatabase>();
            For<IHandler<OrderShipped>>().Use<CreateInvoiceWhenSalesOrderShipped>();
        }
    }
}