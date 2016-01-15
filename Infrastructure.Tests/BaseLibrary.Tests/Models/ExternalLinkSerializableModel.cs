namespace ProcessingTools.BaseLibrary.Tests.Models
{
    using System.Xml.Schema;
    using System.Xml.Serialization;

    [XmlType(AnonymousType = true)]
    [XmlRoot("ext-link", Namespace = "", IsNullable = false)]
    public class ExternalLinkSerializableModel
    {
        [XmlAttribute("ext-link-type")]
        public string ExtLinkType { get; set; }

        [XmlAttribute(attributeName: "href", Form = XmlSchemaForm.Qualified, Namespace = "http://www.w3.org/1999/xlink")]
        public string Href { get; set; }

        [XmlText]
        public string Value { get; set; }
    }
}