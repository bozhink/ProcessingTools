namespace ProcessingTools.Bio.Taxonomy.Data.Xml.Models
{
    using System.Xml.Serialization;

    [XmlType(AnonymousType = true)]
    [XmlRoot(Namespace = "", IsNullable = false, ElementName = "taxa")]
    public class RankListXmlModel
    {
        [XmlElement("taxon")]
        public TaxonXmlModel[] Taxa { get; set; }
    }
}
