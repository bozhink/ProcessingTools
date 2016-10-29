namespace ProcessingTools.Processors.Models.Bio.Codes
{
    using System.Xml.Serialization;

    [XmlType(AnonymousType = true)]
    [XmlRoot(ProcessingTools.Nlm.Publishing.Constants.ElementNames.NamedContent, Namespace = "", IsNullable = false)]
    public class BiorepositoriesInstitutionSerializableModel : NamedContentSerializableModel
    {
        [XmlAttribute(ProcessingTools.Nlm.Publishing.Constants.AttributeNames.ContentType)]
        public override string ContentType
        {
            get
            {
                return ProcessingTools.Constants.Schema.AttributeValues.BiorepositoriesInstitutionContentType;
            }

            set
            {
            }
        }
    }
}
