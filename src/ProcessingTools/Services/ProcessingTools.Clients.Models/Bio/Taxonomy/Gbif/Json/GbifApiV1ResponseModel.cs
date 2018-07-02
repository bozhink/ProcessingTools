// <copyright file="GbifApiV1ResponseModel.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Clients.Models.Bio.Taxonomy.Gbif.Json
{
    using System.Runtime.Serialization;

    /// <summary>
    /// GBIF API v1 response model.
    /// </summary>
    [DataContract]
    public class GbifApiV1ResponseModel
    {
        /// <summary>
        /// Gets or sets a value indicating whether end of records is reached.
        /// </summary>
        [DataMember(Name = "endOfRecords")]
        public bool EndOfRecords { get; set; }

        /// <summary>
        /// Gets or sets limit.
        /// </summary>
        [DataMember(Name = "limit")]
        public int Limit { get; set; }

        /// <summary>
        /// Gets or sets offset.
        /// </summary>
        [DataMember(Name = "offset")]
        public int Offset { get; set; }

        /// <summary>
        /// Gets or sets results.
        /// </summary>
        [DataMember(Name = "results")]
        public GbifApiV1ResultModel[] Results { get; set; }
    }
}
