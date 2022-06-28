// <copyright file="GbifApiV10ResponseModel.cs" company="ProcessingTools">
// Copyright (c) 2022 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Bio.Taxonomy.External.GbifApiV10.Models
{
    using System.Collections.Generic;
    using System.Text.Json;
    using System.Text.Json.Serialization;

    /// <summary>
    /// GBIF API v1 response model.
    /// </summary>
    public class GbifApiV10ResponseModel
    {
        /// <summary>
        /// Gets a value indicating whether end of records is reached.
        /// </summary>
        [JsonPropertyName("endOfRecords")]
        public bool? EndOfRecords { get; init; }

        /// <summary>
        /// Gets the limit number of requested results.
        /// </summary>
        [JsonPropertyName("limit")]
        public int? Limit { get; init; }

        /// <summary>
        /// Gets the offset number of requested results.
        /// </summary>
        [JsonPropertyName("offset")]
        public int? Offset { get; init; }

        /// <summary>
        /// Gets the list of results.
        /// </summary>
        [JsonPropertyName("results")]
        public List<GbifApiV10ResultModel>? Results { get; init; }

        [JsonExtensionData]
        public Dictionary<string, JsonElement>? ExtensionData { get; set; }

    }
}
