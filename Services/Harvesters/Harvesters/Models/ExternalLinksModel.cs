namespace ProcessingTools.Harvesters.Models
{
    using System;
    using System.Xml.Serialization;

    [Serializable]
    [XmlType(AnonymousType = true)]
    [XmlRoot(Namespace = "", IsNullable = false, ElementName = "external-links")]
    public class ExternalLinksModel
    {
        [XmlElement("external-link")]
        public ExternalLinkModel[] ExternalLinks { get; set; }
    }
}