// <copyright file="AcceptedName.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Clients.Models.Bio.Taxonomy.CatalogueOfLife.Xml
{
    using System;
    using System.Xml.Serialization;

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
        [XmlArray("classification")]
        [XmlArrayItem("taxon", typeof(Taxon))]
        public Taxon[] Classification { get; set; }

        /// <summary>
        /// Gets or sets child taxa.
        /// </summary>
        [XmlArray("child_taxa")]
        [XmlArrayItem("taxon", typeof(Taxon))]
        public Taxon[] ChildTaxa { get; set; }

        /// <summary>
        /// Gets or sets synonyms.
        /// </summary>
        [XmlArray("synonyms")]
        [XmlArrayItem("taxon", typeof(SingleRecord))]
        public SingleRecord[] Synonyms { get; set; }

        /// <summary>
        /// Gets or sets common names.
        /// </summary>
        [XmlArray("common_names")]
        [XmlArrayItem("taxon", typeof(CommonName))]
        public CommonName[] CommonNames { get; set; }
    }
}
