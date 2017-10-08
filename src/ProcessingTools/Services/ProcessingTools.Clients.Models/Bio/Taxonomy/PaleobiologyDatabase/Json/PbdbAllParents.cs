// <copyright file="PbdbAllParents.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Clients.Models.Bio.Taxonomy.PaleobiologyDatabase.Json
{
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    /// <summary>
    /// PBDB response model.
    /// </summary>
    [DataContract]
    public class PbdbAllParents
    {
        /// <summary>
        /// Gets or sets records.
        /// </summary>
        [DataMember(Name = "records")]
        public ICollection<PbdbSingleName> Records { get; set; }
    }
}
