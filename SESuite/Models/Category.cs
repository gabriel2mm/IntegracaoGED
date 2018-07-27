using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace testeSuites.Models
{
    [XmlRoot(ElementName = "item", Namespace = "urn:document")]
    public class Item
    {
        [XmlElement(ElementName = "CDCATEGORY", Namespace = "urn:document")]
        public string CDCATEGORY { get; set; }
        [XmlElement(ElementName = "CDCATEGORYOWNER", Namespace = "urn:document")]
        public string CDCATEGORYOWNER { get; set; }
        [XmlElement(ElementName = "IDCATEGORY", Namespace = "urn:document")]
        public string IDCATEGORY { get; set; }
        [XmlElement(ElementName = "NMCATEGORY", Namespace = "urn:document")]
        public string NMCATEGORY { get; set; }
    }

    [XmlRoot(ElementName = "RESULTARRAY", Namespace = "urn:document")]
    public class RESULTARRAY
    {
        [XmlElement(ElementName = "item", Namespace = "urn:document")]
        public List<Item> Item { get; set; }
    }

    [XmlRoot(ElementName = "return", Namespace = "urn:document")]
    public class Return
    {
        [XmlElement(ElementName = "RESULTARRAY", Namespace = "urn:document")]
        public RESULTARRAY RESULTARRAY { get; set; }
    }

    [XmlRoot(ElementName = "searchCategoryResponse", Namespace = "urn:document")]
    public class SearchCategoryResponse
    {
        [XmlElement(ElementName = "return", Namespace = "urn:document")]
        public Return Return { get; set; }
        [XmlAttribute(AttributeName = "xmlns")]
        public string Xmlns { get; set; }
    }

    [XmlRoot(ElementName = "Body", Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
    public class Body
    {
        [XmlElement(ElementName = "searchCategoryResponse", Namespace = "urn:document")]
        public SearchCategoryResponse SearchCategoryResponse { get; set; }
    }

    [XmlRoot(ElementName = "Envelope", Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
    public class Envelope
    {
        [XmlElement(ElementName = "Body", Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
        public Body Body { get; set; }
        [XmlAttribute(AttributeName = "SOAP-ENV", Namespace = "http://www.w3.org/2000/xmlns/")]
        public string SOAPENV { get; set; }
        [XmlAttribute(AttributeName = "xsd", Namespace = "http://www.w3.org/2000/xmlns/")]
        public string Xsd { get; set; }
        [XmlAttribute(AttributeName = "xsi", Namespace = "http://www.w3.org/2000/xmlns/")]
        public string Xsi { get; set; }
        [XmlAttribute(AttributeName = "SOAP-ENC", Namespace = "http://www.w3.org/2000/xmlns/")]
        public string SOAPENC { get; set; }
    }

}
