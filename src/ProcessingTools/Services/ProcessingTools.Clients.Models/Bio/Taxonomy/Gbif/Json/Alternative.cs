// <copyright file="Alternative.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Clients.Models.Bio.Taxonomy.Gbif.Json
{
    using System.Runtime.Serialization;

    /// <summary>
    /// Alternative.
    /// </summary>
    [DataContract]
    public class Alternative : IGbifTaxon
    {
        /// <summary>
        /// Gets or sets usage key.
        /// </summary>
        [DataMember(Name = "usageKey")]
        public int UsageKey { get; set; }

        /// <summary>
        /// Gets or sets scientific name.
        /// </summary>
        [DataMember(Name = "scientificName")]
        public string ScientificName { get; set; }

        /// <summary>
        /// Gets or sets canonical name.
        /// </summary>
        [DataMember(Name = "canonicalName")]
        public string CanonicalName { get; set; }

        /// <summary>
        /// Gets or sets rank.
        /// </summary>
        [DataMember(Name = "rank")]
        public string Rank { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether alternative is synonym.
        /// </summary>
        [DataMember(Name = "synonym")]
        public bool Synonym { get; set; }

        /// <summary>
        /// Gets or sets confidence.
        /// </summary>
        [DataMember(Name = "confidence")]
        public int Confidence { get; set; }

        /// <summary>
        /// Gets or sets note.
        /// </summary>
        [DataMember(Name = "note")]
        public string Note { get; set; }

        /// <summary>
        /// Gets or sets match type.
        /// </summary>
        [DataMember(Name = "matchType")]
        public string MatchType { get; set; }

        /// <summary>
        /// Gets or sets kingdom.
        /// </summary>
        [DataMember(Name = "kingdom")]
        public string Kingdom { get; set; }

        /// <summary>
        /// Gets or sets phylum.
        /// </summary>
        [DataMember(Name = "phylum")]
        public string Phylum { get; set; }

        /// <summary>
        /// Gets or sets class.
        /// </summary>
        [DataMember(Name = "class")]
        public string Class { get; set; }

        /// <summary>
        /// Gets or sets order.
        /// </summary>
        [DataMember(Name = "order")]
        public string Order { get; set; }

        /// <summary>
        /// Gets or sets family.
        /// </summary>
        [DataMember(Name = "family")]
        public string Family { get; set; }

        /// <summary>
        /// Gets or sets genus.
        /// </summary>
        [DataMember(Name = "genus")]
        public string Genus { get; set; }

        /// <summary>
        /// Gets or sets kingdom key.
        /// </summary>
        [DataMember(Name = "kingdomKey")]
        public int KingdomKey { get; set; }

        /// <summary>
        /// Gets or sets phylum key.
        /// </summary>
        [DataMember(Name = "phylumKey")]
        public int PhylumKey { get; set; }

        /// <summary>
        /// Gets or sets class key.
        /// </summary>
        [DataMember(Name = "classKey")]
        public int ClassKey { get; set; }

        /// <summary>
        /// Gets or sets order key.
        /// </summary>
        [DataMember(Name = "orderKey")]
        public int OrderKey { get; set; }

        /// <summary>
        /// Gets or sets family key.
        /// </summary>
        [DataMember(Name = "familyKey")]
        public int FamilyKey { get; set; }

        /// <summary>
        /// Gets or sets genus key.
        /// </summary>
        [DataMember(Name = "genusKey")]
        public int GenusKey { get; set; }
    }
}
