namespace ProcessingTools.Harvesters.Models.ExternalLinks
{
    using System;
    using System.ComponentModel;
    using System.Xml.Serialization;

    using ProcessingTools.Constants.Schema;

    [Serializable]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true, Namespace = Namespaces.ExternalLinksNamespace)]
    [XmlRoot(ElementName = "external-link", Namespace = Namespaces.ExternalLinksNamespace, IsNullable = false)]
    public class ExternalLinkModel : IExternalLinkModel
    {
        [XmlAttribute("base-address")]
        public string BaseAddress { get; set; }

        [XmlAttribute("uri")]
        public string Uri { get; set; }

        [XmlText]
        public string Value { get; set; }
    }
}
