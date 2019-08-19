using System.Collections.Generic;
using System.Xml.Serialization;

namespace TestOrderProjectWebAPI.Models.RequestOrderAggregate
{
    [XmlRoot(ElementName = "orders", Namespace = "")]
    public class OrderRequestArrayModel
    {
        [XmlElement(ElementName = "order")]
        public List<OrderRequestModel> Orders { get; set; }
    }
}