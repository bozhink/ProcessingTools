// <copyright file="DistrictSynonym.cs" company="ProcessingTools">
// Copyright (c) 2021 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Models.Entity.Geo
{
    /// <summary>
    /// District synonym.
    /// </summary>
    public class DistrictSynonym : Synonym
    {
        /// <summary>
        /// Gets or sets the ID of the district.
        /// </summary>
        public virtual int DistrictId { get; set; }

        /// <summary>
        /// Gets or sets the district.
        /// </summary>
        public virtual District District { get; set; }
    }
}
