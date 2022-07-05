// <copyright file="GbifApiV10ResponseModel.cs" company="ProcessingTools">
// Copyright (c) 2022 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Integrations.Gbif.IntegrationModels.V10
{
    using System.Collections.Generic;
    using System.Text.Json.Serialization;
    using ProcessingTools.Integrations.Common.IntegrationModels;

    /// <summary>
    /// GBIF API v1.0 response model.
    /// </summary>
    public class GbifApiV10ResponseModel : BaseJsonResponseModel
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
    }
}
