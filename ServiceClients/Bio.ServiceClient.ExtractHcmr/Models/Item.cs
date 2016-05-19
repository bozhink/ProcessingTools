namespace ProcessingTools.Bio.ServiceClient.ExtractHcmr.Models
{
    using System.Xml.Serialization;

    [XmlType(AnonymousType = true, Namespace = "Reflect")]
    [XmlRoot(ElementName = "item", Namespace = "Reflect", IsNullable = false)]
    public class Item
    {
        [XmlElement(ElementName = "name", Namespace = "Reflect", IsNullable = false)]
        public string Name { get; set; }

        [XmlElement(ElementName = "count", Namespace = "Reflect", IsNullable = false)]
        public byte Count { get; set; }

        [XmlArray(ElementName = "entities", Namespace = "Reflect", IsNullable = false)]
        [XmlArrayItem(ElementName = "entity", Namespace = "Reflect", IsNullable = false)]
        public Entity[] Entities { get; set; }
    }
}