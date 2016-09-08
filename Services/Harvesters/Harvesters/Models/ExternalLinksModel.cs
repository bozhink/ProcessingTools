﻿namespace ProcessingTools.Harvesters.Models
{
    using System;
    using System.ComponentModel;
    using System.Xml.Serialization;

    using ProcessingTools.Common.Constants;

    [Serializable]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true, Namespace = Namespaces.ExternalLinksNamespace)]
    [XmlRoot(ElementName = "external-links", Namespace = Namespaces.ExternalLinksNamespace, IsNullable = false)]
    public class ExternalLinksModel
    {
        [XmlElement("external-link")]
        public ExternalLinkModel[] ExternalLinks { get; set; }
    }
}
