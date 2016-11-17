namespace ProcessingTools.Processors.Models.Bio.Codes
{
    using ProcessingTools.Constants.Schema;
    using System.Xml.Serialization;

    [XmlType(AnonymousType = true)]
    [XmlRoot(ElementNames.NamedContent, Namespace = "", IsNullable = false)]
    public class SpecimenCodeSerializableModel
    {
        [XmlAttribute(AttributeNames.ContentType)]
        public string ContentType
        {
            get
            {
                return AttributeValues.SpecimenCode;
            }

            set
            {
                // Read only
            }
        }

        [XmlAttribute(AttributeNames.XLinkHref, Namespace = Namespaces.XlinkNamespaceUri)]
        public string Href { get; set; }

        [XmlAttribute(AttributeNames.XLinkTitle, Namespace = Namespaces.XlinkNamespaceUri)]
        public string Title { get; set; }

        [XmlText]
        public string Value { get; set; }

        public bool ShouldSerializeHref()
        {
            return !string.IsNullOrWhiteSpace(this.Href);
        }

        public bool ShouldSerializeTitle()
        {
            return !string.IsNullOrWhiteSpace(this.Title);
        }
    }
}
