// <copyright file="ExternalLinkModel.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Harvesters.Models.ExternalLinks
{
    using System;
    using System.ComponentModel;
    using System.Xml.Serialization;
    using ProcessingTools.Constants.Schema;
    using ProcessingTools.Contracts.Models.Harvesters.ExternalLinks;

    /// <summary>
    /// External Link Model.
    /// </summary>
    [Serializable]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true, Namespace = Namespaces.ExternalLinksNamespace)]
    [XmlRoot(ElementName = "external-link", Namespace = Namespaces.ExternalLinksNamespace, IsNullable = false)]
    public class ExternalLinkModel : IExternalLinkModel
    {
        /// <inheritdoc/>
        [XmlAttribute("base-address")]
        public string BaseAddress { get; set; }

        /// <inheritdoc/>
        [XmlAttribute("uri")]
        public string Uri { get; set; }

        /// <inheritdoc/>
        [XmlText]
        public string Value { get; set; }

        /// <inheritdoc/>
        public string FullAddress
        {
            get
            {
                if (string.IsNullOrWhiteSpace(this.BaseAddress))
                {
                    return this.Uri.Trim();
                }
                else
                {
                    return $"{this.BaseAddress.Trim().TrimEnd('/', ' ')}/{this.Uri.Trim().TrimStart('/', ' ')}";
                }
            }
        }
    }
}
