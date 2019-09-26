// <copyright file="CatalogueOfLifeApiServiceXmlResponseModel.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Clients.Models.Bio.Taxonomy.CatalogueOfLife
{
    using System;
    using System.Xml;
    using System.Xml.Serialization;

    /// <summary>
    /// Catalogue of Life API service response.
    /// </summary>
    [Serializable]
    [XmlType(AnonymousType = true)]
    [XmlRoot(Namespace = "", IsNullable = false, ElementName = "results")]
    public class CatalogueOfLifeApiServiceXmlResponseModel
    {
        /// <summary>
        /// Gets or sets ID.
        /// </summary>
        [XmlAttribute("id")]
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets name.
        /// </summary>
        [XmlAttribute("name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets total number of results.
        /// </summary>
        [XmlAttribute("total_number_of_results")]
        public long TotalNumberOfResults { get; set; }

        /// <summary>
        /// Gets or sets number of results returned.
        /// </summary>
        [XmlAttribute("number_of_results_returned")]
        public long NumberOfResultsReturned { get; set; }

        /// <summary>
        /// Gets or sets start.
        /// </summary>
        [XmlAttribute("start")]
        public long Start { get; set; }

        /// <summary>
        /// Gets or sets error message.
        /// </summary>
        [XmlAttribute("error_message")]
        public string ErrorMessage { get; set; }

        /// <summary>
        /// Gets or sets version.
        /// </summary>
        [XmlAttribute("version")]
        public string Version { get; set; }

        /// <summary>
        /// Gets or sets rank.
        /// </summary>
        [XmlAttribute("rank")]
        public string Rank { get; set; }

        /// <summary>
        /// Gets or sets result.
        /// </summary>
        [XmlElement("result", IsNullable = true)]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA2235:Mark all non-serializable fields.", Justification = "XML array")]
        public Result[] Results { get; set; }

        /// <summary>
        /// Result.
        /// </summary>
        [Serializable]
        [XmlType(AnonymousType = true)]
        [XmlRoot(Namespace = "", IsNullable = false, ElementName = "result")]
        public class Result : AcceptedName
        {
            /// <summary>
            /// Gets or sets accepted name.
            /// </summary>
            [XmlElement("accepted_name", typeof(Result), IsNullable = true)]
            public Result AcceptedName { get; set; }
        }

        /// <summary>
        /// Single record.
        /// </summary>
        [Serializable]
        [XmlType(AnonymousType = true)]
        [XmlRoot(Namespace = "", IsNullable = false)]
        public class SingleRecord
        {
            /// <summary>
            /// Gets or sets ID.
            /// </summary>
            [XmlElement("id", IsNullable = true)]
            public string Id { get; set; }

            /// <summary>
            /// Gets or sets name.
            /// </summary>
            [XmlElement("name", IsNullable = true)]
            public string Name { get; set; }

            /// <summary>
            /// Gets or sets name.
            /// </summary>
            [XmlElement("rank", IsNullable = true)]
            public string Rank { get; set; }

            /// <summary>
            /// Gets or sets name.
            /// </summary>
            [XmlElement("genus", IsNullable = true)]
            public string Genus { get; set; }

            /// <summary>
            /// Gets or sets subgenus.
            /// </summary>
            [XmlElement("subgenus", IsNullable = true)]
            public string Subgenus { get; set; }

            /// <summary>
            /// Gets or sets species.
            /// </summary>
            [XmlElement("species", IsNullable = true)]
            public string Species { get; set; }

            /// <summary>
            /// Gets or sets infraspecies marker.
            /// </summary>
            [XmlElement("infraspecies_marker", IsNullable = true)]
            public string InfraspeciesMarker { get; set; }

            /// <summary>
            /// Gets or sets infraspecies.
            /// </summary>
            [XmlElement("infraspecies", IsNullable = true)]
            public string Infraspecies { get; set; }

            /// <summary>
            /// Gets or sets author.
            /// </summary>
            [XmlElement("author", IsNullable = true)]
            public string Author { get; set; }

            /// <summary>
            /// Gets or sets record scrutiny date.
            /// </summary>
            [XmlElement("record_scrutiny_date", typeof(RecordScrutinyDate), IsNullable = true)]
            public RecordScrutinyDate RecordScrutinyDate { get; set; }

            /// <summary>
            /// Gets or sets on-line resource.
            /// </summary>
            [XmlElement("online_resource", IsNullable = true)]
            public string OnlineResource { get; set; }

            /// <summary>
            /// Gets or sets a value indicating whether taxon is extinct.
            /// </summary>
            [XmlElement("is_extinct")]
            public bool IsExtinct { get; set; }

            /// <summary>
            /// Gets or sets source database.
            /// </summary>
            [XmlElement("source_database", IsNullable = true)]
            public string SourceDatabase { get; set; }

            /// <summary>
            /// Gets or sets source database URL.
            /// </summary>
            [XmlElement("source_database_url", IsNullable = true)]
            public string SourceDatabaseUrl { get; set; }

            /// <summary>
            /// Gets or sets bibliographic citation.
            /// </summary>
            [XmlElement("bibliographic_citation", IsNullable = true)]
            public string BibliographicCitation { get; set; }

            /// <summary>
            /// Gets or sets distribution.
            /// </summary>
            [XmlElement("distribution", IsNullable = true)]
            public string Distribution { get; set; }

            /// <summary>
            /// Gets or sets name status.
            /// </summary>
            [XmlElement("name_status", IsNullable = true)]
            public string NameStatus { get; set; }

            /// <summary>
            /// Gets or sets URL.
            /// </summary>
            [XmlElement("url", IsNullable = true)]
            public string Url { get; set; }

            /// <summary>
            /// Gets or sets references.
            /// </summary>
            [XmlArray("references", IsNullable = true)]
            [XmlArrayItem("reference", typeof(Reference), IsNullable = true)]
            [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA2235:Mark all non-serializable fields.", Justification = "XML array")]
            public Reference[] References { get; set; }
        }

        /// <summary>
        /// Accepted name.
        /// </summary>
        [Serializable]
        [XmlType(AnonymousType = true)]
        [XmlRoot(Namespace = "", IsNullable = false, ElementName = "accepted_name")]
        public class AcceptedName : SingleRecord
        {
            /// <summary>
            /// Gets or sets classification.
            /// </summary>
            [XmlArray("classification", IsNullable = true)]
            [XmlArrayItem("taxon", typeof(Taxon), IsNullable = true)]
            [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA2235:Mark all non-serializable fields.", Justification = "XML array")]
            public Taxon[] Classification { get; set; }

            /// <summary>
            /// Gets or sets child taxa.
            /// </summary>
            [XmlArray("child_taxa", IsNullable = true)]
            [XmlArrayItem("taxon", typeof(Taxon), IsNullable = true)]
            [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA2235:Mark all non-serializable fields.", Justification = "XML array")]
            public Taxon[] ChildTaxa { get; set; }

            /// <summary>
            /// Gets or sets synonyms.
            /// </summary>
            [XmlArray("synonyms", IsNullable = true)]
            [XmlArrayItem("taxon", typeof(SingleRecord), IsNullable = true)]
            [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA2235:Mark all non-serializable fields.", Justification = "XML array")]
            public SingleRecord[] Synonyms { get; set; }

            /// <summary>
            /// Gets or sets common names.
            /// </summary>
            [XmlArray("common_names", IsNullable = true)]
            [XmlArrayItem("taxon", typeof(CommonName), IsNullable = true)]
            [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA2235:Mark all non-serializable fields.", Justification = "XML array")]
            public CommonName[] CommonNames { get; set; }
        }

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
            [XmlElement("name", IsNullable = true)]
            public string Name { get; set; }

            /// <summary>
            /// Gets or sets language.
            /// </summary>
            [XmlElement("language", IsNullable = true)]
            public string Language { get; set; }

            /// <summary>
            /// Gets or sets country.
            /// </summary>
            [XmlElement("country", IsNullable = true)]
            public string Country { get; set; }

            /// <summary>
            /// Gets or sets references.
            /// </summary>
            [XmlArray("references", IsNullable = true)]
            [XmlArrayItem("reference", typeof(Reference), IsNullable = true)]
            [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA2235:Mark all non-serializable fields.", Justification = "XML array")]
            public Reference[] References { get; set; }
        }

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
            [XmlElement("id", IsNullable = true)]
            public string Id { get; set; }

            /// <summary>
            /// Gets or sets name.
            /// </summary>
            [XmlElement("name", IsNullable = true)]
            public string Name { get; set; }

            /// <summary>
            /// Gets or sets rank.
            /// </summary>
            [XmlElement("rank", IsNullable = true)]
            public string Rank { get; set; }

            /// <summary>
            /// Gets or sets URL.
            /// </summary>
            [XmlElement("url", IsNullable = true)]
            public string Url { get; set; }

            /// <summary>
            /// Gets or sets a value indicating whether taxon is extinct.
            /// </summary>
            [XmlElement("is_extinct")]
            public bool IsExtinct { get; set; }
        }

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
            public int Year { get; set; }

            /// <summary>
            /// Gets or sets title.
            /// </summary>
            [XmlAnyElement("title")]
            [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA2235:Mark all non-serializable fields.", Justification = "XML array")]
            public XmlNode[] Title { get; set; }

            /// <summary>
            /// Gets or sets source.
            /// </summary>
            [XmlAnyElement("source")]
            [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA2235:Mark all non-serializable fields.", Justification = "XML array")]
            public XmlNode[] Source { get; set; }
        }

        /// <summary>
        /// Record scrutiny date.
        /// </summary>
        [Serializable]
        [XmlType(AnonymousType = true)]
        [XmlRoot(Namespace = "", IsNullable = false, ElementName = "record_scrutiny_date")]
        public partial class RecordScrutinyDate
        {
            /// <summary>
            /// Gets or sets scrutiny.
            /// </summary>
            [XmlElement("scrutiny", IsNullable = true)]
            public string Scrutiny { get; set; }
        }
    }
}
