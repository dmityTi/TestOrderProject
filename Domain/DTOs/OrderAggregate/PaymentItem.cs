namespace Domain.DTOs.OrderAggregate
{
    public class PaymentItem
    {
        public string MethodName { get; set; }
        public decimal Amount { get; set; }
    }
}