namespace ProcessingTools.Models.Serialization.Nlm
{
    using System.Xml.Schema;
    using System.Xml.Serialization;
    using ProcessingTools.Constants.Schema;

    [XmlType(AnonymousType = true)]
    [XmlRoot(ElementName = ElementNames.Abbrev, Namespace = "", IsNullable = false)]
    public partial class AbbreviationXmlModel
    {
        [XmlAttribute(AttributeName = AttributeNames.ContentType)]
        public string ContentType { get; set; }

        [XmlAttribute(AttributeName = AttributeNames.XLinkTitle, Form = XmlSchemaForm.Qualified, Namespace = Namespaces.XlinkNamespaceUri)]
        public string Title { get; set; }

        [XmlText]
        public string Value { get; set; }
    }
}
