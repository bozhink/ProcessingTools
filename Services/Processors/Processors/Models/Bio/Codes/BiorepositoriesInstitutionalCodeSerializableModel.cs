namespace ProcessingTools.Processors.Models.Bio.Codes
{
    using System.Xml.Serialization;
    using ProcessingTools.Constants.Schema;

    [XmlType(AnonymousType = true)]
    [XmlRoot(ElementNames.InstitutionalCode, Namespace = "", IsNullable = false)]
    public class BiorepositoriesInstitutionalCodeSerializableModel
    {
        [XmlAttribute(AttributeNames.Description)]
        public string Description { get; set; }

        [XmlAttribute(AttributeNames.Url)]
        public string Url { get; set; }

        [XmlText]
        public string Value { get; set; }
    }
}
