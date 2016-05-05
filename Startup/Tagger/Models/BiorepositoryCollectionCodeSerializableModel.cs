namespace ProcessingTools.Tagger.Models
{
    using System.Xml.Serialization;
    using ProcessingTools.Nlm.Publishing.Constants;

    [XmlType(AnonymousType = true)]
    [XmlRoot(NodeNames.NamedContent, Namespace = "", IsNullable = false)]
    public class BiorepositoryCollectionCodeSerializableModel : NamedContentSerializableModel
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
    }
}
