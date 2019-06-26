// <copyright file="EnvoEntity.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Models.Entity.Bio.Environments
{
    using System.Collections.Generic;

    /// <summary>
    /// ENVO entity.
    /// </summary>
    public class EnvoEntity
    {
        /// <summary>
        /// Gets or sets the ID of the ENVO entity.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the index of the ENVO entity.
        /// </summary>
        public int Index { get; set; }

        /// <summary>
        /// Gets or sets the ENVO ID.
        /// </summary>
        public string EnvoId { get; set; }

        /// <summary>
        /// Gets or sets the collection of related ENVO names.
        /// </summary>
        public virtual ICollection<EnvoName> Names { get; set; } = new HashSet<EnvoName>();

        /// <summary>
        /// Gets or sets the collection of first groups.
        /// </summary>
        public virtual ICollection<EnvoGroup> Groups1 { get; set; } = new HashSet<EnvoGroup>();

        /// <summary>
        /// Gets or sets the collection of second groups.
        /// </summary>
        public virtual ICollection<EnvoGroup> Groups2 { get; set; } = new HashSet<EnvoGroup>();
    }
}
