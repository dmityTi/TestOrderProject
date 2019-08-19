namespace Domain.Entities
{
    public class Article : BaseEntity
    {
        public string Nomenclature { get; set; }
        public string Title { get; set; }
        public int Amount { get; set; }
        public decimal BrutPrice { get; set; }
        public long OrderId { get; set; }

        public Order Order { get; set; }
    }
}