namespace Demo.Domain
{
    public class OrderShipped : IEvent
    {
        public int OrderId { get; set; }
        public ShippingType ShippingType { get; set; }
    }
}