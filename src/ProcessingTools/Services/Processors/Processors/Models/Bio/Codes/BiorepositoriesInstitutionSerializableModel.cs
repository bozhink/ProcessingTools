namespace ProcessingTools.Processors.Models.Bio.Codes
{
    using System.Xml.Serialization;
    using ProcessingTools.Constants.Schema;

    [XmlType(AnonymousType = true)]
    [XmlRoot(ElementNames.NamedContent, Namespace = "", IsNullable = false)]
    public class BiorepositoriesInstitutionSerializableModel : NamedContentSerializableModel
    {
        [XmlAttribute(AttributeNames.ContentType)]
        public override string ContentType
        {
            get
            {
                return AttributeValues.BiorepositoriesInstitutionContentType;
            }

            set
            {
            }
        }
    }
}
