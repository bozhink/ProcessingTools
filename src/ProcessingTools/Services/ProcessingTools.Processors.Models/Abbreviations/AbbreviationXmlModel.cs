// <copyright file="AbbreviationXmlModel.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Processors.Models.Abbreviations
{
    using System.Xml.Schema;
    using System.Xml.Serialization;
    using ProcessingTools.Common.Constants.Schema;

    /// <summary>
    /// Abbreviation XML model.
    /// </summary>
    [XmlType(AnonymousType = true)]
    [XmlRoot(ElementName = ElementNames.Abbrev, Namespace = "", IsNullable = false)]
    public partial class AbbreviationXmlModel
    {
        /// <summary>
        /// Gets or sets the @content-type.
        /// </summary>
        [XmlAttribute(AttributeName = AttributeNames.ContentType)]
        public string ContentType { get; set; }

        /// <summary>
        /// Gets or sets the @title.
        /// </summary>
        [XmlAttribute(AttributeName = AttributeNames.XLinkTitle, Form = XmlSchemaForm.Qualified, Namespace = Namespaces.XlinkNamespaceUri)]
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the text value.
        /// </summary>
        [XmlText]
        public string Value { get; set; }
    }
}
