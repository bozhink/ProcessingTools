// <copyright file="GbifApiV1ResultModel.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Clients.Models.Bio.Taxonomy.Gbif.Json
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// GBIF API v1 result model.
    /// </summary>
    [DataContract]
    public class GbifApiV1ResultModel
    {
        /// <summary>
        /// Gets or sets according to.
        /// </summary>
        [DataMember(Name = "accordingTo")]
        public string AccordingTo { get; set; }

        /// <summary>
        /// Gets or sets authorship.
        /// </summary>
        [DataMember(Name = "authorship")]
        public string Authorship { get; set; }

        /// <summary>
        /// Gets or sets basionym.
        /// </summary>
        [DataMember(Name = "basionym")]
        public string Basionym { get; set; }

        /// <summary>
        /// Gets or sets basionym key.
        /// </summary>
        [DataMember(Name = "basionymKey")]
        public int BasionymKey { get; set; }

        /// <summary>
        /// Gets or sets canonical name.
        /// </summary>
        [DataMember(Name = "canonicalName")]
        public string CanonicalName { get; set; }

        /// <summary>
        /// Gets or sets class.
        /// </summary>
        [DataMember(Name = "class")]
        public string Class { get; set; }

        /// <summary>
        /// Gets or sets class key.
        /// </summary>
        [DataMember(Name = "classKey")]
        public int ClassKey { get; set; }

        /// <summary>
        /// Gets or sets constituent key.
        /// </summary>
        [DataMember(Name = "constituentKey")]
        public string ConstituentKey { get; set; }

        /// <summary>
        /// Gets or sets dataset key.
        /// </summary>
        [DataMember(Name = "datasetKey")]
        public string DatasetKey { get; set; }

        /// <summary>
        /// Gets or sets family.
        /// </summary>
        [DataMember(Name = "family")]
        public string Family { get; set; }

        /// <summary>
        /// Gets or sets family key.
        /// </summary>
        [DataMember(Name = "familyKey")]
        public int FamilyKey { get; set; }

        /// <summary>
        /// Gets or sets genus.
        /// </summary>
        [DataMember(Name = "genus")]
        public string Genus { get; set; }

        /// <summary>
        /// Gets or sets genus key.
        /// </summary>
        [DataMember(Name = "genusKey")]
        public int GenusKey { get; set; }

        /// <summary>
        /// Gets or sets issues.
        /// </summary>
        [DataMember(Name = "issues")]
        public string[] Issues { get; set; }

        /// <summary>
        /// Gets or sets key.
        /// </summary>
        [DataMember(Name = "key")]
        public int Key { get; set; }

        /// <summary>
        /// Gets or sets kingdom.
        /// </summary>
        [DataMember(Name = "kingdom")]
        public string Kingdom { get; set; }

        /// <summary>
        /// Gets or sets kingdom key.
        /// </summary>
        [DataMember(Name = "kingdomKey")]
        public int KingdomKey { get; set; }

        /// <summary>
        /// Gets or sets last crawled.
        /// </summary>
        [DataMember(Name = "lastCrawled")]
        public DateTime LastCrawled { get; set; }

        /// <summary>
        /// Gets or sets last interpreted.
        /// </summary>
        [DataMember(Name = "lastInterpreted")]
        public DateTime LastInterpreted { get; set; }

        /// <summary>
        /// Gets or sets name type.
        /// </summary>
        [DataMember(Name = "nameType")]
        public string NameType { get; set; }

        /// <summary>
        /// Gets or sets nomenclatural status.
        /// </summary>
        [DataMember(Name = "nomenclaturalStatus")]
        public object[] NomenclaturalStatus { get; set; }

        /// <summary>
        /// Gets or sets nub key.
        /// </summary>
        [DataMember(Name = "nubKey")]
        public int NubKey { get; set; }

        /// <summary>
        /// Gets or sets num descendants.
        /// </summary>
        [DataMember(Name = "numDescendants")]
        public int NumDescendants { get; set; }

        /// <summary>
        /// Gets or sets order.
        /// </summary>
        [DataMember(Name = "order")]
        public string Order { get; set; }

        /// <summary>
        /// Gets or sets order key.
        /// </summary>
        [DataMember(Name = "orderKey")]
        public int OrderKey { get; set; }

        /// <summary>
        /// Gets or sets origin.
        /// </summary>
        [DataMember(Name = "origin")]
        public string Origin { get; set; }

        /// <summary>
        /// Gets or sets parent.
        /// </summary>
        [DataMember(Name = "parent")]
        public string Parent { get; set; }

        /// <summary>
        /// Gets or sets parent key.
        /// </summary>
        [DataMember(Name = "parentKey")]
        public int ParentKey { get; set; }

        /// <summary>
        /// Gets or sets phylum.
        /// </summary>
        [DataMember(Name = "phylum")]
        public string Phylum { get; set; }

        /// <summary>
        /// Gets or sets phylum key.
        /// </summary>
        [DataMember(Name = "phylumKey")]
        public int PhylumKey { get; set; }

        /// <summary>
        /// Gets or sets published in.
        /// </summary>
        [DataMember(Name = "publishedIn")]
        public string PublishedIn { get; set; }

        /// <summary>
        /// Gets or sets rank.
        /// </summary>
        [DataMember(Name = "rank")]
        public string Rank { get; set; }

        /// <summary>
        /// Gets or sets references.
        /// </summary>
        [DataMember(Name = "references")]
        public string References { get; set; }

        /// <summary>
        /// Gets or sets remarks.
        /// </summary>
        [DataMember(Name = "remarks")]
        public string Remarks { get; set; }

        /// <summary>
        /// Gets or sets scientific name.
        /// </summary>
        [DataMember(Name = "scientificName")]
        public string ScientificName { get; set; }

        /// <summary>
        /// Gets or sets source taxon key.
        /// </summary>
        [DataMember(Name = "sourceTaxonKey")]
        public int SourceTaxonKey { get; set; }

        /// <summary>
        /// Gets or sets species.
        /// </summary>
        [DataMember(Name = "species")]
        public string Species { get; set; }

        /// <summary>
        /// Gets or sets species key.
        /// </summary>
        [DataMember(Name = "speciesKey")]
        public int SpeciesKey { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether name is synonym.
        /// </summary>
        [DataMember(Name = "synonym")]
        public bool Synonym { get; set; }

        /// <summary>
        /// Gets or sets taxon ID.
        /// </summary>
        [DataMember(Name = "taxonID")]
        public string TaxonID { get; set; }

        /// <summary>
        /// Gets or sets taxonomic status.
        /// </summary>
        [DataMember(Name = "taxonomicStatus")]
        public string TaxonomicStatus { get; set; }
    }
}
