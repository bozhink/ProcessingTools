namespace ProcessingTools.Bio.Taxonomy.Data.Xml.Models
{
    using System.Xml.Serialization;

    [XmlType(AnonymousType = true)]
    [XmlRoot(Namespace = "", IsNullable = false, ElementName = "list")]
    public class BlackListXmlModel
    {
        [XmlElement("item")]
        public string[] Items { get; set; }
    }
}
