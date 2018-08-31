// <copyright file="NamedContentSerializableModel.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Processors.Models
{
    using System.Xml.Schema;
    using System.Xml.Serialization;
    using ProcessingTools.Common.Constants.Schema;

    /// <summary>
    /// Named-content serializable model.
    /// </summary>
    [XmlType(AnonymousType = true)]
    [XmlRoot(ElementNames.NamedContent, Namespace = "", IsNullable = false)]
    public abstract class NamedContentSerializableModel : SerializableModelWithXLinkTypeSimple
    {
        /// <summary>
        /// Gets or sets the XML text value.
        /// </summary>
        [XmlText]
        public string Value { get; set; }

        /// <summary>
        /// Gets or sets the @content-type.
        /// </summary>
        [XmlAttribute(AttributeNames.ContentType)]
        public abstract string ContentType { get; set; }

        /// <summary>
        /// Gets or sets the xlink:href value.
        /// </summary>
        [XmlAttribute(AttributeNames.XLinkHref, Form = XmlSchemaForm.Qualified, Namespace = Namespaces.XlinkNamespaceUri)]
        public string Url { get; set; }
    }
}
