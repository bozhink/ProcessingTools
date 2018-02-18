// <copyright file="ExternalLinkXmlModel.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Processors.Models.ExternalLinks
{
    using System.Xml.Schema;
    using System.Xml.Serialization;
    using ProcessingTools.Constants.Schema;

    /// <summary>
    /// External link XML model.
    /// </summary>
    [XmlType(AnonymousType = true)]
    [XmlRoot(ElementNames.ExtLink, Namespace = "", IsNullable = false)]
    public class ExternalLinkXmlModel
    {
        /// <summary>
        /// Gets or sets the @ext-link-type.
        /// </summary>
        [XmlAttribute(AttributeNames.ExtLinkType)]
        public string ExternalLinkType { get; set; }

        /// <summary>
        /// Gets or sets the @xlink:href.
        /// </summary>
        [XmlAttribute(AttributeNames.XLinkHref, Form = XmlSchemaForm.Qualified, Namespace = Namespaces.XlinkNamespaceUri)]
        public string Href { get; set; }

        /// <summary>
        /// Gets or sets the text value.
        /// </summary>
        [XmlText]
        public string Value { get; set; }
    }
}
