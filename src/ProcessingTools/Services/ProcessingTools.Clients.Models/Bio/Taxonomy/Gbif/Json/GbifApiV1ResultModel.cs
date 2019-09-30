// <copyright file="GbifApiV1ResultModel.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Clients.Models.Bio.Taxonomy.Gbif.Json
{
    using System;
    using Newtonsoft.Json;

    /// <summary>
    /// GBIF API v1 result model.
    /// </summary>
    public class GbifApiV1ResultModel
    {
        /// <summary>
        /// Gets or sets according to.
        /// </summary>
        [JsonProperty("accordingTo")]
        public string AccordingTo { get; set; }

        /// <summary>
        /// Gets or sets authorship.
        /// </summary>
        [JsonProperty("authorship")]
        public string Authorship { get; set; }

        /// <summary>
        /// Gets or sets basionym.
        /// </summary>
        [JsonProperty("basionym")]
        public string Basionym { get; set; }

        /// <summary>
        /// Gets or sets basionym key.
        /// </summary>
        [JsonProperty("basionymKey")]
        public int BasionymKey { get; set; }

        /// <summary>
        /// Gets or sets canonical name.
        /// </summary>
        [JsonProperty("canonicalName")]
        public string CanonicalName { get; set; }

        /// <summary>
        /// Gets or sets class.
        /// </summary>
        [JsonProperty("class")]
        public string Class { get; set; }

        /// <summary>
        /// Gets or sets class key.
        /// </summary>
        [JsonProperty("classKey")]
        public int ClassKey { get; set; }

        /// <summary>
        /// Gets or sets constituent key.
        /// </summary>
        [JsonProperty("constituentKey")]
        public string ConstituentKey { get; set; }

        /// <summary>
        /// Gets or sets dataset key.
        /// </summary>
        [JsonProperty("datasetKey")]
        public string DatasetKey { get; set; }

        /// <summary>
        /// Gets or sets family.
        /// </summary>
        [JsonProperty("family")]
        public string Family { get; set; }

        /// <summary>
        /// Gets or sets family key.
        /// </summary>
        [JsonProperty("familyKey")]
        public int FamilyKey { get; set; }

        /// <summary>
        /// Gets or sets genus.
        /// </summary>
        [JsonProperty("genus")]
        public string Genus { get; set; }

        /// <summary>
        /// Gets or sets genus key.
        /// </summary>
        [JsonProperty("genusKey")]
        public int GenusKey { get; set; }

        /// <summary>
        /// Gets or sets issues.
        /// </summary>
        [JsonProperty("issues")]
        public string[] Issues { get; set; }

        /// <summary>
        /// Gets or sets key.
        /// </summary>
        [JsonProperty("key")]
        public int Key { get; set; }

        /// <summary>
        /// Gets or sets kingdom.
        /// </summary>
        [JsonProperty("kingdom")]
        public string Kingdom { get; set; }

        /// <summary>
        /// Gets or sets kingdom key.
        /// </summary>
        [JsonProperty("kingdomKey")]
        public int KingdomKey { get; set; }

        /// <summary>
        /// Gets or sets last crawled.
        /// </summary>
        [JsonProperty("lastCrawled")]
        public DateTime LastCrawled { get; set; }

        /// <summary>
        /// Gets or sets last interpreted.
        /// </summary>
        [JsonProperty("lastInterpreted")]
        public DateTime LastInterpreted { get; set; }

        /// <summary>
        /// Gets or sets name type.
        /// </summary>
        [JsonProperty("nameType")]
        public string NameType { get; set; }

        /// <summary>
        /// Gets or sets nomenclatural status.
        /// </summary>
        [JsonProperty("nomenclaturalStatus")]
        public object[] NomenclaturalStatus { get; set; }

        /// <summary>
        /// Gets or sets nub key.
        /// </summary>
        [JsonProperty("nubKey")]
        public int NubKey { get; set; }

        /// <summary>
        /// Gets or sets num descendants.
        /// </summary>
        [JsonProperty("numDescendants")]
        public int NumDescendants { get; set; }

        /// <summary>
        /// Gets or sets order.
        /// </summary>
        [JsonProperty("order")]
        public string Order { get; set; }

        /// <summary>
        /// Gets or sets order key.
        /// </summary>
        [JsonProperty("orderKey")]
        public int OrderKey { get; set; }

        /// <summary>
        /// Gets or sets origin.
        /// </summary>
        [JsonProperty("origin")]
        public string Origin { get; set; }

        /// <summary>
        /// Gets or sets parent.
        /// </summary>
        [JsonProperty("parent")]
        public string Parent { get; set; }

        /// <summary>
        /// Gets or sets parent key.
        /// </summary>
        [JsonProperty("parentKey")]
        public int ParentKey { get; set; }

        /// <summary>
        /// Gets or sets phylum.
        /// </summary>
        [JsonProperty("phylum")]
        public string Phylum { get; set; }

        /// <summary>
        /// Gets or sets phylum key.
        /// </summary>
        [JsonProperty("phylumKey")]
        public int PhylumKey { get; set; }

        /// <summary>
        /// Gets or sets published in.
        /// </summary>
        [JsonProperty("publishedIn")]
        public string PublishedIn { get; set; }

        /// <summary>
        /// Gets or sets rank.
        /// </summary>
        [JsonProperty("rank")]
        public string Rank { get; set; }

        /// <summary>
        /// Gets or sets references.
        /// </summary>
        [JsonProperty("references")]
        public string References { get; set; }

        /// <summary>
        /// Gets or sets remarks.
        /// </summary>
        [JsonProperty("remarks")]
        public string Remarks { get; set; }

        /// <summary>
        /// Gets or sets scientific name.
        /// </summary>
        [JsonProperty("scientificName")]
        public string ScientificName { get; set; }

        /// <summary>
        /// Gets or sets source taxon key.
        /// </summary>
        [JsonProperty("sourceTaxonKey")]
        public int SourceTaxonKey { get; set; }

        /// <summary>
        /// Gets or sets species.
        /// </summary>
        [JsonProperty("species")]
        public string Species { get; set; }

        /// <summary>
        /// Gets or sets species key.
        /// </summary>
        [JsonProperty("speciesKey")]
        public int SpeciesKey { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether name is synonym.
        /// </summary>
        [JsonProperty("synonym")]
        public bool Synonym { get; set; }

        /// <summary>
        /// Gets or sets taxon ID.
        /// </summary>
        [JsonProperty("taxonID")]
        public string TaxonID { get; set; }

        /// <summary>
        /// Gets or sets taxonomic status.
        /// </summary>
        [JsonProperty("taxonomicStatus")]
        public string TaxonomicStatus { get; set; }
    }
}
