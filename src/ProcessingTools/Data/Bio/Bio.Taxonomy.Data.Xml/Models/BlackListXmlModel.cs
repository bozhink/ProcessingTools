namespace ProcessingTools.Bio.Taxonomy.Data.Xml.Models
{
    using System.Xml.Serialization;
    using ProcessingTools.Common.Constants.Data.Bio.Taxonomy;

    [XmlType(AnonymousType = true)]
    [XmlRoot(Namespace = "", IsNullable = false, ElementName = XmlModelsConstants.BlackListXmlRootNodeName)]
    public class BlackListXmlModel
    {
        [XmlElement(XmlModelsConstants.BlackListXmlItemElementName)]
        public string[] Items { get; set; }
    }
}
