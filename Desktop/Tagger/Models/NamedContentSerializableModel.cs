namespace ProcessingTools.Tagger.Models
{
    using System.Xml.Schema;
    using System.Xml.Serialization;
    using ProcessingTools.Nlm.Publishing.Constants;

    [XmlType(AnonymousType = true)]
    [XmlRoot(ElementNames.NamedContent, Namespace = "", IsNullable = false)]
    public abstract class NamedContentSerializableModel : SerializableModelWithXLinkTypeSimple
    {
        [XmlText]
        public string Value { get; set; }

        [XmlAttribute(AttributeNames.ContentType)]
        public abstract string ContentType { get; set; }

        [XmlAttribute(AttributeNames.XLinkHref, Form = XmlSchemaForm.Qualified, Namespace = Namespaces.XlinkNamespaceUri)]
        public string Url { get; set; }
    }
}
