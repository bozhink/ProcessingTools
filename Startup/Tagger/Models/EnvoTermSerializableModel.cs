namespace ProcessingTools.MainProgram.Models
{
    using System.Xml.Serialization;

    [XmlType(AnonymousType = true)]
    [XmlRoot("envo", Namespace = "", IsNullable = false)]
    public class EnvoTermSerializableModel
    {
        [XmlText]
        public string Value { get; set; }

        [XmlAttribute("ID")]
        public string Id { get; set; }

        [XmlAttribute("EnvoID")]
        public string EnvoId { get; set; }

        [XmlAttribute("VerbatimTerm")]
        public string VerbatimTerm { get; set; }
    }
}