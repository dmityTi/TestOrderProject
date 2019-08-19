using System.Collections.Generic;
using System.Xml.Serialization;

namespace TestOrderProjectWebAPI.Models.RequestOrderAggregate
{
    public class Payment
    {
        [XmlElement(ElementName = "method-name")]
        public string MethodName { get; set; }

        [XmlElement(ElementName = "amount")]
        public decimal Amount { get; set; }
    }
    
    public class PaymentList
    {
        [XmlElement(ElementName = "payment")]
        public List<Payment> Payments { get; set; }
    }
}