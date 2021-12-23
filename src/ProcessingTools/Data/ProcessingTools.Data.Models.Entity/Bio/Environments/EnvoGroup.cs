// <copyright file="EnvoGroup.cs" company="ProcessingTools">
// Copyright (c) 2022 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Models.Entity.Bio.Environments
{
    /// <summary>
    /// ENVO group.
    /// </summary>
    public class EnvoGroup
    {
        /// <summary>
        /// Gets or sets the ID of the first ENVO entity in the relation.
        /// </summary>
        public virtual string Entity1Id { get; set; }

        /// <summary>
        /// Gets or sets the first ENVO entity in the relation.
        /// </summary>
        public virtual EnvoEntity Entity1 { get; set; }

        /// <summary>
        /// Gets or sets the ID of the second ENVO entity in the relation.
        /// </summary>
        public virtual string Entity2Id { get; set; }

        /// <summary>
        /// Gets or sets the second ENVO entity in the relation.
        /// </summary>
        public virtual EnvoEntity Entity2 { get; set; }
    }
}
