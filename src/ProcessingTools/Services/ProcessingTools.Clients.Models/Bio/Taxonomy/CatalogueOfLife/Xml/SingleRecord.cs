// <copyright file="SingleRecord.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Clients.Models.Bio.Taxonomy.CatalogueOfLife.Xml
{
    using System;
    using System.Xml;
    using System.Xml.Serialization;

    /// <summary>
    /// Single record.
    /// </summary>
    [Serializable]
    [XmlType(AnonymousType = true)]
    [XmlRoot(Namespace = "", IsNullable = false)]
    public abstract class SingleRecord
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
        /// Gets or sets name.
        /// </summary>
        [XmlElement("rank")]
        public string Rank { get; set; }

        /// <summary>
        /// Gets or sets name.
        /// </summary>
        [XmlElement("genus")]
        public string Genus { get; set; }

        /// <summary>
        /// Gets or sets subgenus.
        /// </summary>
        [XmlElement("subgenus")]
        public string Subgenus { get; set; }

        /// <summary>
        /// Gets or sets species.
        /// </summary>
        [XmlElement("species")]
        public string Species { get; set; }

        /// <summary>
        /// Gets or sets infraspecies marker.
        /// </summary>
        [XmlElement("infraspecies_marker")]
        public string InfraspeciesMarker { get; set; }

        /// <summary>
        /// Gets or sets infraspecies.
        /// </summary>
        [XmlElement("infraspecies")]
        public string Infraspecies { get; set; }

        /// <summary>
        /// Gets or sets author.
        /// </summary>
        [XmlElement("author")]
        public string Author { get; set; }

        /// <summary>
        /// Gets or sets record scrutiny date.
        /// </summary>
        [XmlElement("record_scrutiny_date")]
        public RecordScrutinyDate RecordScrutinyDate { get; set; }

        /// <summary>
        /// Gets or sets online resource.
        /// </summary>
        [XmlElement("online_resource")]
        public string OnlineResource { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether taxon is extinct.
        /// </summary>
        [XmlElement("is_extinct")]
        public bool IsExtinct { get; set; }

        /// <summary>
        /// Gets or sets source database.
        /// </summary>
        [XmlElement("source_database")]
        public string SourceDatabase { get; set; }

        /// <summary>
        /// Gets or sets source database URL.
        /// </summary>
        [XmlElement("source_database_url")]
        public string SourceDatabaseUrl { get; set; }

        /// <summary>
        /// Gets or sets bibliographic citation.
        /// </summary>
        [XmlElement("bibliographic_citation")]
        public string BibliographicCitation { get; set; }

        /// <summary>
        /// Gets or sets distribution.
        /// </summary>
        [XmlElement("distribution")]
        public string Distribution { get; set; }

        /// <summary>
        /// Gets or sets name status.
        /// </summary>
        [XmlElement("name_status")]
        public string NameStatus { get; set; }

        /// <summary>
        /// Gets or sets HTML-encoded name.
        /// </summary>
        [XmlAnyElement("name_html")]
        public XmlNode[] NameHtml { get; set; }

        /// <summary>
        /// Gets or sets URL.
        /// </summary>
        [XmlElement("url")]
        public string Url { get; set; }

        /// <summary>
        /// Gets or sets references.
        /// </summary>
        [XmlArray("references")]
        [XmlArrayItem("reference", typeof(Reference))]
        public Reference[] References { get; set; }
    }
}
