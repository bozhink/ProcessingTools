// <copyright file="Author.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Clients.Models.Bio.Taxonomy.ZooBank.Json
{
    using System.Runtime.Serialization;

    /// <summary>
    /// Author.
    /// </summary>
    [DataContract]
    public class Author
    {
        /// <summary>
        /// Gets or sets family name.
        /// </summary>
        [DataMember(Name = "familyname")]
        public string Familyname { get; set; }

        /// <summary>
        /// Gets or sets given name.
        /// </summary>
        [DataMember(Name = "givenname")]
        public string Givenname { get; set; }

        /// <summary>
        /// Gets or sets GNUB uuid.
        /// </summary>
        [DataMember(Name = "gnubuuid")]
        public string GnubUuid { get; set; }
    }
}
