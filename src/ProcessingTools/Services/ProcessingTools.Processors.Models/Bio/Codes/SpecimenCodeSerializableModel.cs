// <copyright file="SpecimenCodeSerializableModel.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Processors.Models.Bio.Codes
{
    using System.Xml.Serialization;
    using ProcessingTools.Common.Constants.Schema;

    /// <summary>
    /// Specimen code serializable model.
    /// </summary>
    [XmlType(AnonymousType = true)]
    [XmlRoot(ElementNames.NamedContent, Namespace = "", IsNullable = false)]
    public class SpecimenCodeSerializableModel
    {
        /// <summary>
        /// Gets or sets the @content-type.
        /// </summary>
        [XmlAttribute(AttributeNames.ContentType)]
        public string ContentType
        {
            get
            {
                return AttributeValues.SpecimenCode;
            }

            set
            {
                // Read only
            }
        }

        /// <summary>
        /// Gets or sets @xlink:href.
        /// </summary>
        [XmlAttribute(AttributeNames.XLinkHref, Namespace = Namespaces.XlinkNamespaceUri)]
        public string Href { get; set; }

        /// <summary>
        /// Gets or sets @xlink:title.
        /// </summary>
        [XmlAttribute(AttributeNames.XLinkTitle, Namespace = Namespaces.XlinkNamespaceUri)]
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the XML text value.
        /// </summary>
        [XmlText]
        public string Value { get; set; }

        /// <summary>
        /// Conditional serialization for @xlink:href.
        /// </summary>
        /// <returns>Serialize if is not empty.</returns>
        public bool ShouldSerializeHref()
        {
            return !string.IsNullOrWhiteSpace(this.Href);
        }

        /// <summary>
        /// Conditional serialization for @xlink:title.
        /// </summary>
        /// <returns>Serialize if is not empty.</returns>
        public bool ShouldSerializeTitle()
        {
            return !string.IsNullOrWhiteSpace(this.Title);
        }
    }
}
