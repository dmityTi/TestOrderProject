namespace Domain.Entities
{
    public class Payment : BaseEntity
    {
        public string MethodName { get; set; }
        public decimal Amount { get; set; }
        public long OrderId { get; set; }
        
        public Order Order { get; set; }
    }
}