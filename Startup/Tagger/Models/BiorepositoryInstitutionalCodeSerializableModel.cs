namespace ProcessingTools.Tagger.Models
{
    using System.Xml.Serialization;

    [XmlType(AnonymousType = true)]
    [XmlRoot("institutional_code", Namespace = "", IsNullable = false)]
    public class BiorepositoryInstitutionalCodeSerializableModel
    {
        [XmlText]
        public string Value { get; set; }

        [XmlAttribute("description")]
        public string Description { get; set; }

        [XmlAttribute("url")]
        public string Url { get; set; }
    }
}