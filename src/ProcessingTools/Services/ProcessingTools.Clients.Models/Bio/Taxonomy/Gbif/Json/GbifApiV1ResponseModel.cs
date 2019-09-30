// <copyright file="GbifApiV1ResponseModel.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Clients.Models.Bio.Taxonomy.Gbif.Json
{
    using Newtonsoft.Json;

    /// <summary>
    /// GBIF API v1 response model.
    /// </summary>
    public class GbifApiV1ResponseModel
    {
        /// <summary>
        /// Gets or sets a value indicating whether end of records is reached.
        /// </summary>
        [JsonProperty("endOfRecords")]
        public bool EndOfRecords { get; set; }

        /// <summary>
        /// Gets or sets limit.
        /// </summary>
        [JsonProperty("limit")]
        public int Limit { get; set; }

        /// <summary>
        /// Gets or sets offset.
        /// </summary>
        [JsonProperty("offset")]
        public int Offset { get; set; }

        /// <summary>
        /// Gets or sets results.
        /// </summary>
        [JsonProperty("results")]
        public GbifApiV1ResultModel[] Results { get; set; }
    }
}
