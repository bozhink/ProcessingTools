namespace ProcessingTools.Data.Models.Bio.Taxonomy.Xml
{
    using System.Xml.Serialization;
    using ProcessingTools.Common.Constants.Data.Bio.Taxonomy;

    [XmlType(AnonymousType = true)]
    [XmlRoot(Namespace = "", IsNullable = false, ElementName = XmlModelsConstants.RankListXmlRootNodeName)]
    public class RankListXmlModel
    {
        [XmlElement(XmlModelsConstants.RankListTaxonXmlModelElementName)]
        public TaxonXmlModel[] Taxa { get; set; }
    }
}
