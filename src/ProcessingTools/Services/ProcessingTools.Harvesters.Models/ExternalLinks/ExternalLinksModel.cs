// <copyright file="ExternalLinksModel.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Harvesters.Models.ExternalLinks
{
    using System;
    using System.ComponentModel;
    using System.Xml.Serialization;
    using ProcessingTools.Common.Constants.Schema;

    /// <summary>
    /// External Links Model
    /// </summary>
    [Serializable]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true, Namespace = Namespaces.ExternalLinksNamespace)]
    [XmlRoot(ElementName = "external-links", Namespace = Namespaces.ExternalLinksNamespace, IsNullable = false)]
    public class ExternalLinksModel
    {
        /// <summary>
        /// Gets or sets external links.
        /// </summary>
        [XmlElement("external-link")]
        public ExternalLinkModel[] ExternalLinks { get; set; }
    }
}
