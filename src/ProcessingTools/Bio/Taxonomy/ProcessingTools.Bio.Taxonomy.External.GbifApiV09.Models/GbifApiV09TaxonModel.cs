// <copyright file="GbifApiV09TaxonModel.cs" company="ProcessingTools">
// Copyright (c) 2021 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Bio.Taxonomy.External.GbifApiV09.Models
{
    using System.Text.Json.Serialization;

    /// <summary>
    /// Alternative.
    /// </summary>
    public class GbifApiV09TaxonModel
    {
        /// <summary>
        /// Gets the usage key.
        /// </summary>
        [JsonPropertyName("usageKey")]
        public int? UsageKey { get; init; }

        /// <summary>
        /// Gets the scientific name.
        /// </summary>
        [JsonPropertyName("scientificName")]
        public string? ScientificName { get; init; }

        /// <summary>
        /// Gets the canonical name.
        /// </summary>
        [JsonPropertyName("canonicalName")]
        public string? CanonicalName { get; init; }

        /// <summary>
        /// Gets the rank.
        /// </summary>
        [JsonPropertyName("rank")]
        public string? Rank { get; init; }

        /// <summary>
        /// Gets the a value indicating whether alternative is synonym.
        /// </summary>
        [JsonPropertyName("synonym")]
        public bool? Synonym { get; init; }

        /// <summary>
        /// Gets the confidence.
        /// </summary>
        [JsonPropertyName("confidence")]
        public int? Confidence { get; init; }

        /// <summary>
        /// Gets the note.
        /// </summary>
        [JsonPropertyName("note")]
        public string? Note { get; init; }

        /// <summary>
        /// Gets the match type.
        /// </summary>
        [JsonPropertyName("matchType")]
        public string? MatchType { get; init; }

        /// <summary>
        /// Gets the kingdom.
        /// </summary>
        [JsonPropertyName("kingdom")]
        public string? Kingdom { get; init; }

        /// <summary>
        /// Gets the phylum.
        /// </summary>
        [JsonPropertyName("phylum")]
        public string? Phylum { get; init; }

        /// <summary>
        /// Gets the class.
        /// </summary>
        [JsonPropertyName("class")]
        public string? Class { get; init; }

        /// <summary>
        /// Gets the order.
        /// </summary>
        [JsonPropertyName("order")]
        public string? Order { get; init; }

        /// <summary>
        /// Gets the family.
        /// </summary>
        [JsonPropertyName("family")]
        public string? Family { get; init; }

        /// <summary>
        /// Gets the genus.
        /// </summary>
        [JsonPropertyName("genus")]
        public string? Genus { get; init; }

        /// <summary>
        /// Gets the kingdom key.
        /// </summary>
        [JsonPropertyName("kingdomKey")]
        public int? KingdomKey { get; init; }

        /// <summary>
        /// Gets the phylum key.
        /// </summary>
        [JsonPropertyName("phylumKey")]
        public int? PhylumKey { get; init; }

        /// <summary>
        /// Gets the class key.
        /// </summary>
        [JsonPropertyName("classKey")]
        public int? ClassKey { get; init; }

        /// <summary>
        /// Gets the order key.
        /// </summary>
        [JsonPropertyName("orderKey")]
        public int? OrderKey { get; init; }

        /// <summary>
        /// Gets the family key.
        /// </summary>
        [JsonPropertyName("familyKey")]
        public int? FamilyKey { get; init; }

        /// <summary>
        /// Gets the genus key.
        /// </summary>
        [JsonPropertyName("genusKey")]
        public int? GenusKey { get; init; }
    }
}
