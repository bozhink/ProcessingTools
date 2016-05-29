namespace ProcessingTools.Bio.Taxonomy.Data.Xml.Models
{
    using System.Xml.Serialization;
    using ProcessingTools.Bio.Taxonomy.Data.Common.Constants;

    [XmlType(AnonymousType = true)]
    [XmlRoot(Namespace = "", IsNullable = false, ElementName = XmlModelsConstants.RankListTaxonXmlPartElementName)]
    public class TaxonPartXmlModel
    {
        [XmlElement(XmlModelsConstants.RankListTaxonXmlPartValueElementName)]
        public string Value { get; set; }

        [XmlElement(XmlModelsConstants.RankListTaxonXmlPartRankElementName)]
        public TaxonRankXmlModel Ranks { get; set; }
    }
}
