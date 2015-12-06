namespace ProcessingTools.Bio.Taxonomy.Data.Models.Xml
{
    using System.Xml.Serialization;

    [XmlType(AnonymousType = true)]
    public class Taxon
    {
        [XmlElement("part")]
        public TaxonPart[] Parts { get; set; }
    }
}