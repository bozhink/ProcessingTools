namespace ProcessingTools.Bio.Taxonomy.Data.Xml.Models
{
    using System.Xml.Serialization;
    using ProcessingTools.Bio.Taxonomy.Data.Common.Constants;

    [XmlType(AnonymousType = true)]
    [XmlRoot(Namespace = "", IsNullable = false, ElementName = XmlModelsConstants.RankListXmlRootNodeName)]
    public class RankListXmlModel
    {
        [XmlElement(XmlModelsConstants.RankListTaxonXmlModelElementName)]
        public TaxonXmlModel[] Taxa { get; set; }
    }
}
