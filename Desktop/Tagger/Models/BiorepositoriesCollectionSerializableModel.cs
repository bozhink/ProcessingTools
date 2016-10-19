namespace ProcessingTools.Tagger.Models
{
    using System.Xml.Schema;
    using System.Xml.Serialization;
    using ProcessingTools.Nlm.Publishing.Constants;

    [XmlType(AnonymousType = true)]
    [XmlRoot(ElementNames.NamedContent, Namespace = "", IsNullable = false)]
    public class BiorepositoriesCollectionSerializableModel : NamedContentSerializableModel
    {
        [XmlAttribute(AttributeNames.ContentType)]
        public override string ContentType
        {
            get
            {
                return XmlNodesSettings.Default.BiorepositoriesCollectionContentType;
            }

            set
            {
            }
        }
    }
}
