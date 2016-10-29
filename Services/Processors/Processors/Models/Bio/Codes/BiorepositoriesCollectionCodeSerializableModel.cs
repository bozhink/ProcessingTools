namespace ProcessingTools.Processors.Models.Bio.Codes
{
    using System.Xml.Schema;
    using System.Xml.Serialization;

    [XmlType(AnonymousType = true)]
    [XmlRoot(ProcessingTools.Nlm.Publishing.Constants.ElementNames.NamedContent, Namespace = "", IsNullable = false)]
    public class BiorepositoriesCollectionCodeSerializableModel : NamedContentSerializableModel
    {
        [XmlAttribute(ProcessingTools.Nlm.Publishing.Constants.AttributeNames.ContentType)]
        public override string ContentType
        {
            get
            {
                return ProcessingTools.Constants.Schema.AttributeValues.BiorepositoriesCollectionCodeContentType;
            }

            set
            {
            }
        }

        [XmlAttribute(ProcessingTools.Nlm.Publishing.Constants.AttributeNames.XLinkTitle, Form = XmlSchemaForm.Qualified, Namespace = ProcessingTools.Nlm.Publishing.Constants.Namespaces.XlinkNamespaceUri)]
        public string XLinkTitle { get; set; }
    }
}
