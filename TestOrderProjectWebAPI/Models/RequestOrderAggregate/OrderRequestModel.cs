using System;
using System.Xml.Serialization;

namespace TestOrderProjectWebAPI.Models.RequestOrderAggregate
{
    public class OrderRequestModel
    {
        [XmlElement(ElementName = "oxid")]
        public long Oxid { get; set; }
        
        [XmlElement(ElementName = "orderdate")]
        public DateTime OrderDate { get; set; }
        
        [XmlElement(ElementName = "billingaddress")]
        public BillingAddress BillingAddress { get; set; }
        
        [XmlElement(ElementName = "payments")]
        public PaymentList Payments { get; set; }
     
        [XmlElement(ElementName = "articles")]
        public OrderArticleList Articles { get; set; }
    }
}