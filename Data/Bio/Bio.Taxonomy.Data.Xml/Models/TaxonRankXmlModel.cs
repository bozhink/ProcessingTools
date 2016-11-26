namespace ProcessingTools.Bio.Taxonomy.Data.Xml.Models
{
    using System.Xml.Serialization;
    using ProcessingTools.Bio.Taxonomy.Data.Common.Constants;

    [XmlType(AnonymousType = true)]
    [XmlRoot(Namespace = "", IsNullable = false, ElementName = XmlModelsConstants.RankListTaxonXmlPartRankElementName)]
    public class TaxonRankXmlModel
    {
        [XmlElement(XmlModelsConstants.RankListTaxonXmlPartRankValueElementName)]
        public string[] Values { get; set; }
    }
}
