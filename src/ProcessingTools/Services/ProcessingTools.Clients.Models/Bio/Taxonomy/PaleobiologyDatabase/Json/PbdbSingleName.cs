// <copyright file="PbdbSingleName.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Clients.Models.Bio.Taxonomy.PaleobiologyDatabase.Json
{
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    /// <summary>
    /// PBDB taxon name.
    /// </summary>
    [DataContract]
    public class PbdbSingleName
    {
        /// <summary>
        /// Gets or sets object Id.
        /// </summary>
        [DataMember(Name = "oid")]
        public int ObjectId { get; set; }

        /// <summary>
        /// Gets or sets group Id.
        /// </summary>
        [DataMember(Name = "gid")]
        public int GroupId { get; set; }

        /// <summary>
        /// Gets or sets type.
        /// </summary>
        [DataMember(Name = "typ")]
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets rank.
        /// </summary>
        [DataMember(Name = "rnk")]
        public int Rank { get; set; }

        /// <summary>
        /// Gets or sets name.
        /// </summary>
        [DataMember(Name = "nam")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets name 2.
        /// </summary>
        [DataMember(Name = "nm2")]
        public string Name2 { get; set; }

        /// <summary>
        /// Gets or sets status.
        /// </summary>
        [DataMember(Name = "sta")]
        public string Sta { get; set; }

        /// <summary>
        /// Gets or sets parent.
        /// </summary>
        [DataMember(Name = "par")]
        public int Par { get; set; }

        /// <summary>
        /// Gets or sets snr.
        /// </summary>
        [DataMember(Name = "snr")]
        public int Snr { get; set; }

        /// <summary>
        /// Gets or sets reference Id.
        /// </summary>
        [DataMember(Name = "rid")]
        public List<int> Rid { get; set; }

        /// <summary>
        /// Gets or sets ext.
        /// </summary>
        [DataMember(Name = "ext")]
        public int Ext { get; set; }
    }
}
