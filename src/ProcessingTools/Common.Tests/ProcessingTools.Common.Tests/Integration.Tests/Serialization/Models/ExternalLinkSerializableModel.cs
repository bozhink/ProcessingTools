// <copyright file="ExternalLinkSerializableModel.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Common.Tests.Integration.Tests.Serialization.Models
{
    using System.Xml.Schema;
    using System.Xml.Serialization;

    /// <summary>
    /// External Link Serializable Model.
    /// </summary>
    [XmlType(AnonymousType = true)]
    [XmlRoot("ext-link", Namespace = "", IsNullable = false)]
    public class ExternalLinkSerializableModel
    {
        /// <summary>
        /// Gets or sets @ext-link-type.
        /// </summary>
        [XmlAttribute("ext-link-type")]
        public string ExtLinkType { get; set; }

        /// <summary>
        /// Gets or sets @xlink:href.
        /// </summary>
        [XmlAttribute(attributeName: "href", Form = XmlSchemaForm.Qualified, Namespace = "http://www.w3.org/1999/xlink")]
        public string Href { get; set; }

        /// <summary>
        /// Gets or sets text value.
        /// </summary>
        [XmlText]
        public string Value { get; set; }
    }
}
