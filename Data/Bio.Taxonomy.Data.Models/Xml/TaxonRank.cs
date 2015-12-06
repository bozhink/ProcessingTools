namespace ProcessingTools.Bio.Taxonomy.Data.Models.Xml
{
    using System.Xml.Serialization;

    [XmlType(AnonymousType = true)]
    public partial class TaxonRank
    {
        [XmlElement("value")]
        public string[] Values { get; set; }
    }
}