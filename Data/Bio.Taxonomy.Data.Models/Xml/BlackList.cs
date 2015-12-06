namespace ProcessingTools.Bio.Taxonomy.Data.Models.Xml
{
    using System.Xml.Serialization;

    [XmlType(AnonymousType = true)]
    [XmlRoot(Namespace = "", IsNullable = false, ElementName = "list")]
    public class BlackList
    {
        [XmlElement("item")]
        public string[] Items { get; set; }
    }
}
