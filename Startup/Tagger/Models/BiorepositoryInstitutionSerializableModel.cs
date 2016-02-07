namespace ProcessingTools.MainProgram.Models
{
    using System.Xml.Serialization;

    [XmlType(AnonymousType = true)]
    [XmlRoot("institution", Namespace = "", IsNullable = false)]
    public class BiorepositoryInstitutionSerializableModel
    {
        [XmlText]
        public string Value { get; set; }

        [XmlAttribute("url")]
        public string Url { get; set; }
    }
}