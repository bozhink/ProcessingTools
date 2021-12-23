// <copyright file="GbifApiV09ResponseModel.cs" company="ProcessingTools">
// Copyright (c) 2022 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Bio.Taxonomy.External.GbifApiV09.Models
{
    using System.Collections.Generic;
    using System.Text.Json.Serialization;

    /// <summary>
    /// GBIF API v0.9 response model.
    /// </summary>
    public class GbifApiV09ResponseModel : GbifApiV09TaxonModel
    {
        /// <summary>
        /// Gets the list of alternatives.
        /// </summary>
        [JsonPropertyName("alternatives")]
        public List<GbifApiV09TaxonModel>? Alternatives { get; init; }
    }
}
