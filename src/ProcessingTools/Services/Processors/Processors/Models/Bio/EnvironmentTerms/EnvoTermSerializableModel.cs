namespace ProcessingTools.Processors.Models.Bio.EnvironmentTerms
{
    using System.Xml.Serialization;

    [XmlType(AnonymousType = true)]
    [XmlRoot("envo", Namespace = "", IsNullable = false)]
    public class EnvoTermSerializableModel
    {
        [XmlAttribute("EnvoID")]
        public string EnvoId { get; set; }

        [XmlAttribute("ID")]
        public string Id { get; set; }

        [XmlText]
        public string Value { get; set; }

        [XmlAttribute("VerbatimTerm")]
        public string VerbatimTerm { get; set; }
    }
}
