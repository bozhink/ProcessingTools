namespace ProcessingTools.Bio.Taxonomy.Data.Xml.Models
{
    using System.Xml.Serialization;

    [XmlType(AnonymousType = true)]
    [XmlRoot(Namespace = "", IsNullable = false, ElementName = "rank")]
    public class TaxonRankXmlModel
    {
        [XmlElement("value")]
        public string[] Values { get; set; }
    }
}
