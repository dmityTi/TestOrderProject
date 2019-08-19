using System.Collections.Generic;
using System.Xml.Serialization;

namespace TestOrderProjectWebAPI.Models.RequestOrderAggregate
{
    public class OrderArticle
    {
        [XmlElement(ElementName = "artnum")]
        public string ArtNum { get; set; }
        
        [XmlElement(ElementName = "title")]
        public string Title { get; set; }
        
        [XmlElement(ElementName = "amount")]
        public int Amount { get; set; }
        
        [XmlElement(ElementName = "brutprice")]
        public decimal BrutPrice { get; set; }
    }
    
    public class OrderArticleList
    {
        [XmlElement(ElementName = "orderarticle")]
        public List<OrderArticle> Articles { get; set; }
    }
}