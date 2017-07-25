using System;
using System.Collections.Generic;
using System.Linq;
using Demo.Config;
using Demo.Domain;
using Demo.Rop;
using FluentAssertions;
using Xunit;

namespace Demo.Tests
{
    public class ProgramTests
    {
        [Fact]
        public void RunMultipleTimes()
        {
            Ioc.Configure<Demo.V4.V4Registry>();
            var handler = Ioc.Container.GetInstance<V4.CreateInvoiceWhenSalesOrderShipped>();
            var evt = new OrderShipped
            {
                OrderId = 1,
                ShippingType = ShippingType.Standard
            };

            var allResults = new int[100]
                .Select(o => handler.HandleInternal(evt))
                .ToArray();

            var aggregate = allResults.Aggregate();
            aggregate.IsFailure.Should().BeTrue();
            
            Console.WriteLine(new string('-',20));

            Console.WriteLine("Failures: " + string.Join(Environment.NewLine, aggregate.Failure.GroupBy(o => o)
                .OrderByDescending(o => o.Count())
                .Select(o => $"{o.Key}: {o.Count()}")));

            Console.WriteLine(new string('-',20));

            var successes = allResults.Where(o => o.IsSuccess).Aggregate();
            successes.IsSuccess.Should().BeTrue();
            Console.WriteLine($"Successes: {successes.Success.Count()}");
        }
    }
}