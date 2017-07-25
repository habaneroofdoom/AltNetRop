using StructureMap;
using Demo.Domain;

namespace Demo.V4
{
    public class V4Registry : Registry
    {
        public V4Registry()
        {
            ForSingletonOf<IRopDatabase>().Use<V4.RopDatabase>();
            For<IHandler<OrderShipped>>().Use<CreateInvoiceWhenSalesOrderShipped>();
        }
    }
}