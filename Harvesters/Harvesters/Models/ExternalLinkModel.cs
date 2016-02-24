namespace ProcessingTools.Harvesters.Models
{
    using System;
    using System.Xml.Serialization;

    [Serializable]
    [XmlType(AnonymousType = true)]
    [XmlRoot(Namespace = "", IsNullable = false, ElementName = "external-link")]
    public class ExternalLinkModel
    {
        [XmlAttribute("base-address")]
        public string BaseAddress { get; set; }

        [XmlAttribute("uri")]
        public string Uri { get; set; }

        [XmlText]
        public string Value { get; set; }
    }
}