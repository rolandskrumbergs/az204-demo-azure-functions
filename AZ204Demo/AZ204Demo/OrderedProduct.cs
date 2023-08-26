namespace AZ204Demo
{
    public class OrderedProduct : Product
    {
        public int Amount { get; set; }
        public required string Unit { get; set; }
    }
}
