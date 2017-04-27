namespace ProcessingTools.Processors.Models.ExternalLinks
{
    using System.Xml.Schema;
    using System.Xml.Serialization;
    using ProcessingTools.Constants.Schema;

    [XmlType(AnonymousType = true)]
    [XmlRoot(ElementNames.ExtLink, Namespace = "", IsNullable = false)]
    public class ExternalLinkSerializableModel
    {
        [XmlAttribute(AttributeNames.ExtLinkType)]
        public string ExternalLinkType { get; set; }

        [XmlAttribute(AttributeNames.XLinkHref, Form = XmlSchemaForm.Qualified, Namespace = Namespaces.XlinkNamespaceUri)]
        public string Href { get; set; }

        [XmlText]
        public string Value { get; set; }
    }
}
