// <copyright file="RegionSynonym.cs" company="ProcessingTools">
// Copyright (c) 2021 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Models.Entity.Geo
{
    /// <summary>
    /// Region synonym.
    /// </summary>
    public class RegionSynonym : Synonym
    {
        /// <summary>
        /// Gets or sets the ID of the region.
        /// </summary>
        public virtual int RegionId { get; set; }

        /// <summary>
        /// Gets or sets the region.
        /// </summary>
        public virtual Region Region { get; set; }
    }
}
