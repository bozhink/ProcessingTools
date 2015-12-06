namespace ProcessingTools.Bio.Taxonomy.Data.Models.Xml
{
    using System.Xml.Serialization;

    [XmlType(AnonymousType = true)]
    [XmlRoot(Namespace = "", IsNullable = false, ElementName = "taxa")]
    public class RankList
    {
        [XmlElement("taxon")]
        public Taxon[] Taxa { get; set; }
    }
}