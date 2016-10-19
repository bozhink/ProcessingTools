namespace ProcessingTools.Tagger.Models
{
    using System.Xml.Schema;
    using System.Xml.Serialization;
    using ProcessingTools.Nlm.Publishing.Constants;

    [XmlType(AnonymousType = true)]
    [XmlRoot(ElementNames.NamedContent, Namespace = "", IsNullable = false)]
    public class BiorepositoriesCollectionCodeSerializableModel : NamedContentSerializableModel
    {
        [XmlAttribute(AttributeNames.ContentType)]
        public override string ContentType
        {
            get
            {
                return XmlNodesSettings.Default.BiorepositoriesCollectionCodeContentType;
            }

            set
            {
            }
        }

        [XmlAttribute(AttributeNames.XLinkTitle, Form = XmlSchemaForm.Qualified, Namespace = Namespaces.XlinkNamespaceUri)]
        public string XLinkTitle { get; set; }
    }
}
