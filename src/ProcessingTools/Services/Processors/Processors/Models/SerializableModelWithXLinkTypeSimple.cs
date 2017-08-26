namespace ProcessingTools.Processors.Models
{
    using System.Xml.Schema;
    using System.Xml.Serialization;
    using ProcessingTools.Constants.Schema;

    public class SerializableModelWithXLinkTypeSimple
    {
        [XmlAttribute(AttributeNames.XLinkType, Form = XmlSchemaForm.Qualified, Namespace = Namespaces.XlinkNamespaceUri)]
        public string XLinkType
        {
            get
            {
                return ProcessingTools.Enumerations.Nlm.XLinkType.Simple.ToString().ToLowerInvariant();
            }

            set
            {
                // Skip
            }
        }
    }
}
