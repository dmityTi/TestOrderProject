namespace Domain.DTOs.OrderAggregate
{
    public class BillingAddressItem
    {
        public string Email { get; set; }
        public string FullName { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public int HomeNumber { get; set; }
        public int Zip { get; set; }
    }
}