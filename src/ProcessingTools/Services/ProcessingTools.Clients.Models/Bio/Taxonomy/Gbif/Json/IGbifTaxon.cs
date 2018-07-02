// <copyright file="IGbifTaxon.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Clients.Models.Bio.Taxonomy.Gbif.Json
{
    /// <summary>
    /// GBIF taxon.
    /// </summary>
    public interface IGbifTaxon
    {
        /// <summary>
        /// Gets or sets canonical name.
        /// </summary>
        string CanonicalName { get; set; }

        /// <summary>
        /// Gets or sets class.
        /// </summary>
        string Class { get; set; }

        /// <summary>
        /// Gets or sets class key.
        /// </summary>
        int ClassKey { get; set; }

        /// <summary>
        /// Gets or sets confidence.
        /// </summary>
        int Confidence { get; set; }

        /// <summary>
        /// Gets or sets family.
        /// </summary>
        string Family { get; set; }

        /// <summary>
        /// Gets or sets family key.
        /// </summary>
        int FamilyKey { get; set; }

        /// <summary>
        /// Gets or sets genus.
        /// </summary>
        string Genus { get; set; }

        /// <summary>
        /// Gets or sets genus key.
        /// </summary>
        int GenusKey { get; set; }

        /// <summary>
        /// Gets or sets kingdom.
        /// </summary>
        string Kingdom { get; set; }

        /// <summary>
        /// Gets or sets kingdom key.
        /// </summary>
        int KingdomKey { get; set; }

        /// <summary>
        /// Gets or sets match type.
        /// </summary>
        string MatchType { get; set; }

        /// <summary>
        /// Gets or sets note.
        /// </summary>
        string Note { get; set; }

        /// <summary>
        /// Gets or sets order.
        /// </summary>
        string Order { get; set; }

        /// <summary>
        /// Gets or sets order key.
        /// </summary>
        int OrderKey { get; set; }

        /// <summary>
        /// Gets or sets phylum.
        /// </summary>
        string Phylum { get; set; }

        /// <summary>
        /// Gets or sets phylum key.
        /// </summary>
        int PhylumKey { get; set; }

        /// <summary>
        /// Gets or sets rank.
        /// </summary>
        string Rank { get; set; }

        /// <summary>
        /// Gets or sets scientific name.
        /// </summary>
        string ScientificName { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether taxon is synonym.
        /// </summary>
        bool Synonym { get; set; }

        /// <summary>
        /// Gets or sets usage key.
        /// </summary>
        int UsageKey { get; set; }
    }
}
