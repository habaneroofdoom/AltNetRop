using StructureMap;
using Demo.Domain;

namespace Demo.V1
{
    public class V1Registry : Registry
    {
        public V1Registry()
        {
            ForSingletonOf<IDatabase>().Use<V1.Database>();
            For<IHandler<OrderShipped>>().Use<CreateInvoiceWhenSalesOrderShipped>();
        }
    }
}