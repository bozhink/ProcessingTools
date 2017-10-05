// <copyright file="Taxon.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Clients.Models.Bio.Taxonomy.CatalogueOfLife.Xml
{
    using System;
    using System.Xml.Serialization;

    /// <summary>
    /// Taxon.
    /// </summary>
    [Serializable]
    [XmlType(AnonymousType = true)]
    [XmlRoot(Namespace = "", IsNullable = false, ElementName = "taxon")]
    public class Taxon
    {
        /// <summary>
        /// Gets or sets ID.
        /// </summary>
        [XmlElement("id")]
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets name.
        /// </summary>
        [XmlElement("name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets rank.
        /// </summary>
        [XmlElement("rank")]
        public string Rank { get; set; }

        /// <summary>
        /// Gets or sets HTML encoded name.
        /// </summary>
        [XmlElement("name_html")]
        public string NameHtml { get; set; }

        /// <summary>
        /// Gets or sets URL.
        /// </summary>
        [XmlElement("url")]
        public string Url { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether taxon is extinct.
        /// </summary>
        [XmlElement("is_extinct")]
        public bool IsExtinct { get; set; }
    }
}
