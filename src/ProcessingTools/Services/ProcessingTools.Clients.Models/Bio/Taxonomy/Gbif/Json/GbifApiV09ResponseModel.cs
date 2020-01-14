// <copyright file="GbifApiV09ResponseModel.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Clients.Models.Bio.Taxonomy.Gbif.Json
{
    using Newtonsoft.Json;

    /// <summary>
    /// GBIF API v0.9 response model.
    /// </summary>
    public class GbifApiV09ResponseModel : GbifTaxon
    {
        /// <summary>
        /// Gets or sets alternatives.
        /// </summary>
        [JsonProperty("alternatives")]
        public GbifTaxon[] Alternatives { get; set; }
    }
}
