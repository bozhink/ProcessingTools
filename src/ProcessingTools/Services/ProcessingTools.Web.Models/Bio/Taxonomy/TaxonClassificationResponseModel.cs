// <copyright file="TaxonClassificationResponseModel.cs" company="ProcessingTools">
// Copyright (c) 2021 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Models.Bio.Taxonomy
{
    using Newtonsoft.Json;
    using ProcessingTools.Contracts.Models;

    /// <summary>
    /// Represents response model for the taxonomic classification API.
    /// </summary>
    public class TaxonClassificationResponseModel : ISearchResult
    {
        /// <inheritdoc/>
        public string SearchKey { get; set; }

        /// <summary>
        /// Gets or sets aberration.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Aberration { get; set; }

        /// <summary>
        /// Gets or sets authority.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Authority { get; set; }

        /// <summary>
        /// Gets or sets canonical name.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string CanonicalName { get; set; }

        /// <summary>
        /// Gets or sets clade.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Clade { get; set; }

        /// <summary>
        /// Gets or sets class.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Class { get; set; }

        /// <summary>
        /// Gets or sets cohort.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Cohort { get; set; }

        /// <summary>
        /// Gets or sets division.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Division { get; set; }

        /// <summary>
        /// Gets or sets family.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Family { get; set; }

        /// <summary>
        /// Gets or sets form.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Form { get; set; }

        /// <summary>
        /// Gets or sets genus.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Genus { get; set; }

        /// <summary>
        /// Gets or sets infraclass.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Infraclass { get; set; }

        /// <summary>
        /// Gets or sets infradivision.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Infradivision { get; set; }

        /// <summary>
        /// Gets or sets infrakingdom.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Infrakingdom { get; set; }

        /// <summary>
        /// Gets or sets infraorder.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Infraorder { get; set; }

        /// <summary>
        /// Gets or sets infraphylum.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Infraphylum { get; set; }

        /// <summary>
        /// Gets or sets kingdom.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Kingdom { get; set; }

        /// <summary>
        /// Gets or sets order.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Order { get; set; }

        /// <summary>
        /// Gets or sets parvorder.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Parvorder { get; set; }

        /// <summary>
        /// Gets or sets phylum.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Phylum { get; set; }

        /// <summary>
        /// Gets or sets race.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Race { get; set; }

        /// <summary>
        /// Gets or sets rank.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Rank { get; set; }

        /// <summary>
        /// Gets or sets scientific name.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string ScientificName { get; set; }

        /// <summary>
        /// Gets or sets section.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Section { get; set; }

        /// <summary>
        /// Gets or sets series.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Series { get; set; }

        /// <summary>
        /// Gets or sets species.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Species { get; set; }

        /// <summary>
        /// Gets or sets stage.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Stage { get; set; }

        /// <summary>
        /// Gets or sets subclade.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Subclade { get; set; }

        /// <summary>
        /// Gets or sets subclass.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Subclass { get; set; }

        /// <summary>
        /// Gets or sets subdivision.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Subdivision { get; set; }

        /// <summary>
        /// Gets or sets subfamily.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Subfamily { get; set; }

        /// <summary>
        /// Gets or sets subform.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Subform { get; set; }

        /// <summary>
        /// Gets or sets subgenus.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Subgenus { get; set; }

        /// <summary>
        /// Gets or sets subkingdom.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Subkingdom { get; set; }

        /// <summary>
        /// Gets or sets suborder.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Suborder { get; set; }

        /// <summary>
        /// Gets or sets subphylum.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Subphylum { get; set; }

        /// <summary>
        /// Gets or sets subsection.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Subsection { get; set; }

        /// <summary>
        /// Gets or sets subseries.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Subseries { get; set; }

        /// <summary>
        /// Gets or sets subspecies.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Subspecies { get; set; }

        /// <summary>
        /// Gets or sets subtribe.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Subtribe { get; set; }

        /// <summary>
        /// Gets or sets subvariety.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Subvariety { get; set; }

        /// <summary>
        /// Gets or sets superclass.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Superclass { get; set; }

        /// <summary>
        /// Gets or sets superfamily.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Superfamily { get; set; }

        /// <summary>
        /// Gets or sets supergroup.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Supergroup { get; set; }

        /// <summary>
        /// Gets or sets superkingdom.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Superkingdom { get; set; }

        /// <summary>
        /// Gets or sets superorder.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Superorder { get; set; }

        /// <summary>
        /// Gets or sets superphylum.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Superphylum { get; set; }

        /// <summary>
        /// Gets or sets superspecies.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Superspecies { get; set; }

        /// <summary>
        /// Gets or sets supertribe.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Supertribe { get; set; }

        /// <summary>
        /// Gets or sets tribe.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Tribe { get; set; }

        /// <summary>
        /// Gets or sets variety.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Variety { get; set; }
    }
}
