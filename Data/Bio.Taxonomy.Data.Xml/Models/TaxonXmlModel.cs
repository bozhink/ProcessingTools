namespace ProcessingTools.Bio.Taxonomy.Data.Xml.Models
{
    using System.Xml.Serialization;

    [XmlType(AnonymousType = true)]
    [XmlRoot(Namespace = "", IsNullable = false, ElementName = "taxon")]
    public class TaxonXmlModel
    {
        [XmlAttribute("white-listed")]
        public bool IsWhiteListed { get; set; }

        [XmlElement("part")]
        public TaxonPartXmlModel[] Parts { get; set; }
    }
}
