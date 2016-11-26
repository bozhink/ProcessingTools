namespace ProcessingTools.Bio.Taxonomy.Data.Xml.Models
{
    using System.Xml.Serialization;
    using ProcessingTools.Bio.Taxonomy.Data.Common.Constants;

    [XmlType(AnonymousType = true)]
    [XmlRoot(Namespace = "", IsNullable = false, ElementName = XmlModelsConstants.BlackListXmlRootNodeName)]
    public class BlackListXmlModel
    {
        [XmlElement(XmlModelsConstants.BlackListXmlItemElementName)]
        public string[] Items { get; set; }
    }
}
