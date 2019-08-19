using System.Xml.Serialization;

namespace TestOrderProjectWebAPI.Models.RequestOrderAggregate
{
    public class Country
    {
        [XmlElement(ElementName = "geo")]
        public string Geo { get; set; }
    }
}