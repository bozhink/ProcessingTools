namespace ProcessingTools.Bio.Taxonomy.Data.Xml.Models
{
    using System.Xml.Serialization;
    using ProcessingTools.Constants.Data.Bio.Taxonomy;

    [XmlType(AnonymousType = true)]
    [XmlRoot(Namespace = "", IsNullable = false, ElementName = XmlModelsConstants.RankListTaxonXmlPartRankElementName)]
    public class TaxonRankXmlModel
    {
        [XmlElement(XmlModelsConstants.RankListTaxonXmlPartRankValueElementName)]
        public string[] Values { get; set; }
    }
}
