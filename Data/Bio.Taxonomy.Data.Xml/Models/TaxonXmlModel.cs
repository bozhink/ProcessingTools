namespace ProcessingTools.Bio.Taxonomy.Data.Xml.Models
{
    using System.Xml.Serialization;
    using ProcessingTools.Bio.Taxonomy.Data.Common.Constants;

    [XmlType(AnonymousType = true)]
    [XmlRoot(Namespace = "", IsNullable = false, ElementName = XmlModelsConstants.RankListTaxonXmlModelElementName)]
    public class TaxonXmlModel
    {
        public TaxonXmlModel()
        {
            this.IsWhiteListed = false;
        }

        [XmlAttribute(XmlModelsConstants.RankListIsWhiteListedXmlAttributeName)]
        public bool IsWhiteListed { get; set; }

        [XmlElement(XmlModelsConstants.RankListTaxonXmlPartElementName)]
        public TaxonPartXmlModel[] Parts { get; set; }
    }
}
