namespace ProcessingTools.Harvesters.Models
{
    using System;
    using System.ComponentModel;
    using System.Xml.Serialization;

    using ProcessingTools.Common.Constants;

    [Serializable]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true, Namespace = Namespaces.ExternalLinksNamespace)]
    [XmlRoot(ElementName = "external-link", Namespace = Namespaces.ExternalLinksNamespace, IsNullable = false)]
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
