namespace ProcessingTools.Bio.Taxonomy.Data.Xml.Models
{
    using System.Xml.Serialization;

    [XmlType(AnonymousType = true)]
    [XmlRoot(Namespace = "", IsNullable = false, ElementName = "part")]
    public class TaxonPartXmlModel
    {
        [XmlElement("value")]
        public string Value { get; set; }

        [XmlElement("rank")]
        public TaxonRankXmlModel Ranks { get; set; }
    }
}
