namespace ProcessingTools.Tagger.Models
{
    using System.Xml.Schema;
    using System.Xml.Serialization;
    using ProcessingTools.Nlm.Publishing.Constants;

    public class SerializableModelWithXLinkTypeSimple
    {
        [XmlAttribute(AttributeNames.XLinkType, Form = XmlSchemaForm.Qualified, Namespace = Namespaces.XlinkNamespaceUri)]
        public string XLinkType
        {
            get
            {
                return ProcessingTools.Nlm.Publishing.Types.XLinkType.Simple.ToString().ToLower();
            }

            set
            {
            }
        }
    }
}
