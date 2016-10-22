namespace ProcessingTools.Bio.Processors.Models.Taggers
{
    using System.Xml.Serialization;

    [XmlType(AnonymousType = true)]
    [XmlRoot("envo", Namespace = "", IsNullable = false)]
    public class EnvoExtractHcmrSerializableModel
    {
        [XmlAttribute("identifier1")]
        public string Identifier { get; set; }

        [XmlAttribute("type1")]
        public string Type { get; set; }

        [XmlText]
        public string Value { get; set; }
    }
}
