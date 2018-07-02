// <copyright file="Reference.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Clients.Models.Bio.Taxonomy.CatalogueOfLife.Xml
{
    using System;
    using System.Xml;
    using System.Xml.Serialization;

    /// <summary>
    /// Reference.
    /// </summary>
    [Serializable]
    [XmlType(AnonymousType = true)]
    [XmlRoot(Namespace = "", IsNullable = false, ElementName = "reference")]
    public partial class Reference
    {
        /// <summary>
        /// Gets or sets author.
        /// </summary>
        [XmlElement("author")]
        public string Author { get; set; }

        /// <summary>
        /// Gets or sets year.
        /// </summary>
        [XmlElement("year")]
        public string Year { get; set; }

        /// <summary>
        /// Gets or sets title.
        /// </summary>
        [XmlAnyElement("title")]
        public XmlNode[] Title { get; set; }

        /// <summary>
        /// Gets or sets source.
        /// </summary>
        [XmlAnyElement("source")]
        public XmlNode[] Source { get; set; }
    }
}
