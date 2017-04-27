namespace ProcessingTools.Bio.ServiceClient.ExtractHcmr.Models
{
    using System.Xml.Serialization;

    [XmlType(AnonymousType = true, Namespace = "Reflect")]
    [XmlRoot(ElementName = "GetEntitiesResponse", Namespace = "Reflect", IsNullable = false)]
    public class ExtractHcmrResponseModel
    {
        [XmlArray(ElementName = "items", Namespace = "Reflect", IsNullable = false)]
        [XmlArrayItem(ElementName = "item", Namespace = "Reflect", IsNullable = false)]
        public Item[] Items { get; set; }
    }
}