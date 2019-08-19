namespace Domain.DTOs.OrderAggregate
{
    public class ArticleItem
    {
        public string Nomenclature { get; set; }
        public string Title { get; set; }
        public int Amount { get; set; }
        public decimal BrutPrice { get; set; }
    }
}