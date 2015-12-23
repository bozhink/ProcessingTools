namespace ProcessingTools.Bio.ServiceClient.ExtractHcmr.Models
{
    using System.Xml.Serialization;

    [XmlType(AnonymousType = true, Namespace = "Reflect")]
    [XmlRoot(ElementName = "entity", Namespace = "Reflect", IsNullable = false)]
    public class Entity
    {
        [XmlElement(ElementName = "type", Namespace = "Reflect", IsNullable = false)]
        public int Type { get; set; }

        [XmlElement(ElementName = "identifier", Namespace = "Reflect", IsNullable = false)]
        public string Identifier { get; set; }
    }
}