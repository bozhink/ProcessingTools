// <copyright file="EnvoEntity.cs" company="ProcessingTools">
// Copyright (c) 2022 ProcessingTools. All rights reserved.
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
        /// Gets the collection of related ENVO names.
        /// </summary>
        public virtual ICollection<EnvoName> Names { get; private set; } = new HashSet<EnvoName>();

        /// <summary>
        /// Gets the collection of first groups.
        /// </summary>
        public virtual ICollection<EnvoGroup> Groups1 { get; private set; } = new HashSet<EnvoGroup>();

        /// <summary>
        /// Gets the collection of second groups.
        /// </summary>
        public virtual ICollection<EnvoGroup> Groups2 { get; private set; } = new HashSet<EnvoGroup>();
    }
}
