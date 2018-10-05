namespace ProcessingTools.Data.Models.Bio.Taxonomy.Xml
{
    using System.Xml.Serialization;
    using ProcessingTools.Common.Constants.Data.Bio.Taxonomy;

    [XmlType(AnonymousType = true)]
    [XmlRoot(Namespace = "", IsNullable = false, ElementName = XmlModelsConstants.RankListTaxonXmlPartRankElementName)]
    public class TaxonRankXmlModel
    {
        [XmlElement(XmlModelsConstants.RankListTaxonXmlPartRankValueElementName)]
        public string[] Values { get; set; }
    }
}
