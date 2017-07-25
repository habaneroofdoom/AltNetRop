using System;
using Demo.Config;
using Demo.Domain;
using Demo.V1;
using Demo.V2;
using Demo.V3;
using Demo.V4;
using StructureMap;

namespace Demo
{
    class Program
    {
        private static readonly OrderShipped SafeEvt = new OrderShipped
        {
            OrderId = 1,
            ShippingType = ShippingType.Standard
        };

        private static readonly OrderShipped InvalidEvt = new OrderShipped
        {
            OrderId = 2,
            ShippingType = ShippingType.Teleport
        };
        

        static void Main(string[] args)
        {
            // Handle<V1.V1Registry>(InvalidEvt);
            // Handle<V2.V2Registry>(SafeEvt);
            // Handle<V2.V2Registry>(InvalidEvt);
            // Handle<V3.V3Registry>(SafeEvt);
            // Handle<V3.V3Registry>(InvalidEvt);
            Handle<V4.V4Registry>(SafeEvt);
            // Handle<V4.V4Registry>(InvalidEvt);
        }

        private static void Handle<T>(OrderShipped evt) where T : Registry, new()
        {
            Ioc.Configure<T>();
            var handler = Ioc.Container.GetInstance<IHandler<OrderShipped>>();
            handler.Handle(evt);
        }
    }
}
