namespace ProcessingTools.Processors.Models.Bio.Codes
{
    using System.Xml.Serialization;

    [XmlType(AnonymousType = true)]
    [XmlRoot("institutional_code", Namespace = "", IsNullable = false)]
    public class BiorepositoriesInstitutionalCodeSerializableModel
    {
        [XmlText]
        public string Value { get; set; }

        [XmlAttribute("description")]
        public string Description { get; set; }

        [XmlAttribute("url")]
        public string Url { get; set; }
    }
}
