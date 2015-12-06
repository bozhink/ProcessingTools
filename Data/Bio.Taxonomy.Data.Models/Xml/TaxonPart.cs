namespace ProcessingTools.Bio.Taxonomy.Data.Models.Xml
{
    using System.Xml.Serialization;

    [XmlType(AnonymousType = true)]
    [XmlRoot(Namespace = "", IsNullable = false, ElementName = "part")]
    public class TaxonPart
    {
        [XmlElement("value")]
        public string Value { get; set; }

        [XmlElement("rank")]
        public TaxonRank Rank { get; set; }
    }
}