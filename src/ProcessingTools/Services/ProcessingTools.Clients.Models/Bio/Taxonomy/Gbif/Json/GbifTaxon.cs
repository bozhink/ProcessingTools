// <copyright file="GbifTaxon.cs" company="ProcessingTools">
// Copyright (c) 2021 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Clients.Models.Bio.Taxonomy.Gbif.Json
{
    using Newtonsoft.Json;

    /// <summary>
    /// Alternative.
    /// </summary>
    public class GbifTaxon : IGbifTaxon
    {
        /// <summary>
        /// Gets or sets usage key.
        /// </summary>
        [JsonProperty("usageKey")]
        public int UsageKey { get; set; }

        /// <summary>
        /// Gets or sets scientific name.
        /// </summary>
        [JsonProperty("scientificName")]
        public string ScientificName { get; set; }

        /// <summary>
        /// Gets or sets canonical name.
        /// </summary>
        [JsonProperty("canonicalName")]
        public string CanonicalName { get; set; }

        /// <summary>
        /// Gets or sets rank.
        /// </summary>
        [JsonProperty("rank")]
        public string Rank { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether alternative is synonym.
        /// </summary>
        [JsonProperty("synonym")]
        public bool Synonym { get; set; }

        /// <summary>
        /// Gets or sets confidence.
        /// </summary>
        [JsonProperty("confidence")]
        public int Confidence { get; set; }

        /// <summary>
        /// Gets or sets note.
        /// </summary>
        [JsonProperty("note")]
        public string Note { get; set; }

        /// <summary>
        /// Gets or sets match type.
        /// </summary>
        [JsonProperty("matchType")]
        public string MatchType { get; set; }

        /// <summary>
        /// Gets or sets kingdom.
        /// </summary>
        [JsonProperty("kingdom")]
        public string Kingdom { get; set; }

        /// <summary>
        /// Gets or sets phylum.
        /// </summary>
        [JsonProperty("phylum")]
        public string Phylum { get; set; }

        /// <summary>
        /// Gets or sets class.
        /// </summary>
        [JsonProperty("class")]
        public string Class { get; set; }

        /// <summary>
        /// Gets or sets order.
        /// </summary>
        [JsonProperty("order")]
        public string Order { get; set; }

        /// <summary>
        /// Gets or sets family.
        /// </summary>
        [JsonProperty("family")]
        public string Family { get; set; }

        /// <summary>
        /// Gets or sets genus.
        /// </summary>
        [JsonProperty("genus")]
        public string Genus { get; set; }

        /// <summary>
        /// Gets or sets kingdom key.
        /// </summary>
        [JsonProperty("kingdomKey")]
        public int KingdomKey { get; set; }

        /// <summary>
        /// Gets or sets phylum key.
        /// </summary>
        [JsonProperty("phylumKey")]
        public int PhylumKey { get; set; }

        /// <summary>
        /// Gets or sets class key.
        /// </summary>
        [JsonProperty("classKey")]
        public int ClassKey { get; set; }

        /// <summary>
        /// Gets or sets order key.
        /// </summary>
        [JsonProperty("orderKey")]
        public int OrderKey { get; set; }

        /// <summary>
        /// Gets or sets family key.
        /// </summary>
        [JsonProperty("familyKey")]
        public int FamilyKey { get; set; }

        /// <summary>
        /// Gets or sets genus key.
        /// </summary>
        [JsonProperty("genusKey")]
        public int GenusKey { get; set; }
    }
}
