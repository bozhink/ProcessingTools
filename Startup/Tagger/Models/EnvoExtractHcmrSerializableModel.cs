namespace ProcessingTools.MainProgram.Models
{
    using System.Xml.Serialization;

    [XmlType(AnonymousType = true)]
    [XmlRoot("envo", Namespace = "", IsNullable = false)]
    public class EnvoExtractHcmrSerializableModel
    {
        [XmlText]
        public string Value { get; set; }

        [XmlAttribute("type1")]
        public string Type { get; set; }

        [XmlAttribute("identifier1")]
        public string Identifier { get; set; }
    }
}