using System.Xml.Serialization;

namespace TestOrderProjectWebAPI.Models.RequestOrderAggregate
{
    public class BillingAddress
    {
        [XmlElement(ElementName = "billemail")]
        public string Email { get; set; }
        
        [XmlElement(ElementName = "billfname")]
        public string Name { get; set; }
        
        [XmlElement(ElementName = "billstreet")]
        public string Street { get; set; }
        
        [XmlElement(ElementName = "billstreetnr")]
        public int StreetNr { get; set; }
        
        [XmlElement(ElementName = "billcity")]
        public string City { get; set; }
        
        [XmlElement(ElementName = "country")]
        public Country Country { get; set; }

        [XmlElement(ElementName = "billzip")]
        public int Zip { get; set; }
    }
}