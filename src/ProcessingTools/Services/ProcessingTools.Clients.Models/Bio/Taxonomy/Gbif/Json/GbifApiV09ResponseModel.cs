// <copyright file="GbifApiV09ResponseModel.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Clients.Models.Bio.Taxonomy.Gbif.Json
{
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    /// <summary>
    /// GBIF API v0.9 response model.
    /// </summary>
    [DataContract]
    public class GbifApiV09ResponseModel : Alternative
    {
        /// <summary>
        /// Gets or sets alternatives.
        /// </summary>
        [DataMember(Name = "alternatives")]
        public IEnumerable<Alternative> Alternatives { get; set; }
    }
}
