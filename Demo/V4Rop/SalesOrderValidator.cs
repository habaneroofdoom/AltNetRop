using System.Collections.Generic;
using System.Linq;
using Demo.Domain;
using Demo.Rop;
using static Demo.Rop.Result<Demo.Domain.SalesOrder, string[]>;

namespace Demo.V4Rop
{
    public static class SalesOrderValidator
    {
        public static Result<SalesOrder, string[]> ValidateForInvoicing(SalesOrder salesOrder)
        {
            var errors = GetErrors(salesOrder).ToArray();

            return errors.Any()
                ? Failed(errors)
                : Succeeded(salesOrder);
        }

        private static IEnumerable<string> GetErrors(SalesOrder salesOrder)
        {
            if (salesOrder == null)
            {
                yield return "Order is null";
                yield break;
            }

            if (salesOrder.OrderPickDate == null)
                yield return "Order has not been picked, cannot invoice";

            if (salesOrder.Customer == null)
                yield return "Order has no customer";
            else if (salesOrder.Customer.Tariff == null)
                yield return "Cannot determine tariff for customer";
        }
    }
}