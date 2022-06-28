// <copyright file="GbifApiV10ResultModel.cs" company="ProcessingTools">
// Copyright (c) 2022 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Integrations.Gbif.IntegrationModels.V10
{
    using System.Collections.Generic;
    using System.Text.Json.Serialization;
    using ProcessingTools.Integrations.Common.IntegrationModels;

    /// <summary>
    /// GBIF API v1.0 result model.
    /// </summary>
    public class GbifApiV10ResultModel : BaseJsonResponseModel
    {
        /// <summary>
        /// Gets according to.
        /// </summary>
        [JsonPropertyName("accordingTo")]
        public string? AccordingTo { get; init; }

        /// <summary>
        /// Gets the authorship.
        /// </summary>
        [JsonPropertyName("authorship")]
        public string? Authorship { get; init; }

        /// <summary>
        /// Gets the basionym.
        /// </summary>
        [JsonPropertyName("basionym")]
        public string? Basionym { get; init; }

        /// <summary>
        /// Gets the basionym key.
        /// </summary>
        [JsonPropertyName("basionymKey")]
        public int? BasionymKey { get; init; }

        /// <summary>
        /// Gets the canonical name.
        /// </summary>
        [JsonPropertyName("canonicalName")]
        public string? CanonicalName { get; init; }

        /// <summary>
        /// Gets the class.
        /// </summary>
        [JsonPropertyName("class")]
        public string? Class { get; init; }

        /// <summary>
        /// Gets the class key.
        /// </summary>
        [JsonPropertyName("classKey")]
        public int? ClassKey { get; init; }

        /// <summary>
        /// Gets the constituent key.
        /// </summary>
        [JsonPropertyName("constituentKey")]
        public string? ConstituentKey { get; init; }

        /// <summary>
        /// Gets the dataset key.
        /// </summary>
        [JsonPropertyName("datasetKey")]
        public string? DatasetKey { get; init; }

        /// <summary>
        /// Gets the family.
        /// </summary>
        [JsonPropertyName("family")]
        public string? Family { get; init; }

        /// <summary>
        /// Gets the family key.
        /// </summary>
        [JsonPropertyName("familyKey")]
        public int? FamilyKey { get; init; }

        /// <summary>
        /// Gets the genus.
        /// </summary>
        [JsonPropertyName("genus")]
        public string? Genus { get; init; }

        /// <summary>
        /// Gets the genus key.
        /// </summary>
        [JsonPropertyName("genusKey")]
        public int? GenusKey { get; init; }

        /// <summary>
        /// Gets the issues.
        /// </summary>
        [JsonPropertyName("issues")]
        public List<string>? Issues { get; init; }

        /// <summary>
        /// Gets the key.
        /// </summary>
        [JsonPropertyName("key")]
        public int? Key { get; init; }

        /// <summary>
        /// Gets the kingdom.
        /// </summary>
        [JsonPropertyName("kingdom")]
        public string? Kingdom { get; init; }

        /// <summary>
        /// Gets the kingdom key.
        /// </summary>
        [JsonPropertyName("kingdomKey")]
        public int? KingdomKey { get; init; }

        /// <summary>
        /// Gets the date of last crawled.
        /// </summary>
        [JsonPropertyName("lastCrawled")]
        public string? LastCrawled { get; init; }

        /// <summary>
        /// Gets the date of last interpreted.
        /// </summary>
        [JsonPropertyName("lastInterpreted")]
        public string LastInterpreted { get; init; }

        /// <summary>
        /// Gets the name type.
        /// </summary>
        [JsonPropertyName("nameType")]
        public string? NameType { get; init; }

        /// <summary>
        /// Gets the nomenclatural status.
        /// </summary>
        [JsonPropertyName("nomenclaturalStatus")]
        public List<string>? NomenclaturalStatus { get; init; }

        /// <summary>
        /// Gets the nub key.
        /// </summary>
        [JsonPropertyName("nubKey")]
        public int? NubKey { get; init; }

        /// <summary>
        /// Gets the number of descendants.
        /// </summary>
        [JsonPropertyName("numDescendants")]
        public int? NumDescendants { get; init; }

        /// <summary>
        /// Gets the order.
        /// </summary>
        [JsonPropertyName("order")]
        public string? Order { get; init; }

        /// <summary>
        /// Gets the order key.
        /// </summary>
        [JsonPropertyName("orderKey")]
        public int? OrderKey { get; init; }

        /// <summary>
        /// Gets the origin.
        /// </summary>
        [JsonPropertyName("origin")]
        public string? Origin { get; init; }

        /// <summary>
        /// Gets the parent.
        /// </summary>
        [JsonPropertyName("parent")]
        public string? Parent { get; init; }

        /// <summary>
        /// Gets the parent key.
        /// </summary>
        [JsonPropertyName("parentKey")]
        public int? ParentKey { get; init; }

        /// <summary>
        /// Gets the phylum.
        /// </summary>
        [JsonPropertyName("phylum")]
        public string? Phylum { get; init; }

        /// <summary>
        /// Gets the phylum key.
        /// </summary>
        [JsonPropertyName("phylumKey")]
        public int? PhylumKey { get; init; }

        /// <summary>
        /// Gets published in.
        /// </summary>
        [JsonPropertyName("publishedIn")]
        public string? PublishedIn { get; init; }

        /// <summary>
        /// Gets the rank.
        /// </summary>
        [JsonPropertyName("rank")]
        public string? Rank { get; init; }

        /// <summary>
        /// Gets the references.
        /// </summary>
        [JsonPropertyName("references")]
        public string? References { get; init; }

        /// <summary>
        /// Gets the remarks.
        /// </summary>
        [JsonPropertyName("remarks")]
        public string? Remarks { get; init; }

        /// <summary>
        /// Gets the scientific name.
        /// </summary>
        [JsonPropertyName("scientificName")]
        public string? ScientificName { get; init; }

        /// <summary>
        /// Gets the source taxon key.
        /// </summary>
        [JsonPropertyName("sourceTaxonKey")]
        public int? SourceTaxonKey { get; init; }

        /// <summary>
        /// Gets the species.
        /// </summary>
        [JsonPropertyName("species")]
        public string? Species { get; init; }

        /// <summary>
        /// Gets the species key.
        /// </summary>
        [JsonPropertyName("speciesKey")]
        public int? SpeciesKey { get; init; }

        /// <summary>
        /// Gets a value indicating whether name is synonym.
        /// </summary>
        [JsonPropertyName("synonym")]
        public bool? Synonym { get; init; }

        /// <summary>
        /// Gets the taxon ID.
        /// </summary>
        [JsonPropertyName("taxonID")]
        public string? TaxonID { get; init; }

        /// <summary>
        /// Gets the taxonomic status.
        /// </summary>
        [JsonPropertyName("taxonomicStatus")]
        public string? TaxonomicStatus { get; init; }
    }
}
