namespace ProcessingTools.Processors.Models.Bio.Codes
{
    using System.Xml.Serialization;
    using ProcessingTools.Constants.Schema;

    [XmlType(AnonymousType = true)]
    [XmlRoot(ElementNames.NamedContent, Namespace = "", IsNullable = false)]
    public class SpecimenCodeSerializableModel
    {
        [XmlText]
        public string Value { get; set; }

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

        [XmlAttribute(AttributeNames.SpecificUse)]
        public string SpecificUse { get; set; }
    }
}
