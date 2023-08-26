namespace AZ204Demo
{
    public class Order
    {
        public Guid id { get; set; }
        public DateTime Created { get; set; }
        public IEnumerable<OrderedProduct> Products { get; set; } = Enumerable.Empty<OrderedProduct>();
        public required OrderStatus Status { get; set; }
        public required Address DeliveryAddress { get; set; } 

    }
}