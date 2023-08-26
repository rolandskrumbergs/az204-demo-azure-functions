namespace AZ204Demo
{
    public record Address
    {
        public required string Line { get ; set; }
        public required string City { get; set; }
        public required string Country { get; set; }
        public required string PostalCode { get; set; }
    }
}
