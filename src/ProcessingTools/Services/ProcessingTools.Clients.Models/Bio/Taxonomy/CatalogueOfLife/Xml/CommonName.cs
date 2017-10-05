// <copyright file="CommonName.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Clients.Models.Bio.Taxonomy.CatalogueOfLife.Xml
{
    using System;
    using System.Xml.Serialization;

    /// <summary>
    /// Common name.
    /// </summary>
    [Serializable]
    [XmlType(AnonymousType = true)]
    [XmlRoot(Namespace = "", IsNullable = false, ElementName = "common_name")]
    public class CommonName
    {
        /// <summary>
        /// Gets or sets name.
        /// </summary>
        [XmlElement("name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets language.
        /// </summary>
        [XmlElement("language")]
        public string Language { get; set; }

        /// <summary>
        /// Gets or sets country.
        /// </summary>
        [XmlElement("country")]
        public string Country { get; set; }

        /// <summary>
        /// Gets or sets references.
        /// </summary>
        [XmlArray("references")]
        [XmlArrayItem("reference", typeof(Reference))]
        public Reference[] References { get; set; }
    }
}
