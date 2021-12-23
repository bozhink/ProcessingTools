// <copyright file="CountySynonym.cs" company="ProcessingTools">
// Copyright (c) 2022 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Models.Entity.Geo
{
    /// <summary>
    /// County synonym.
    /// </summary>
    public class CountySynonym : Synonym
    {
        /// <summary>
        /// Gets or sets the ID of the county.
        /// </summary>
        public virtual int CountyId { get; set; }

        /// <summary>
        /// Gets or sets the county.
        /// </summary>
        public virtual County County { get; set; }
    }
}
