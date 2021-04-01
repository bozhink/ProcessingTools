// <copyright file="ContinentSynonym.cs" company="ProcessingTools">
// Copyright (c) 2021 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Models.Entity.Geo
{
    /// <summary>
    /// Continent synonym.
    /// </summary>
    public class ContinentSynonym : Synonym
    {
        /// <summary>
        /// Gets or sets the ID of the continent.
        /// </summary>
        public virtual int ContinentId { get; set; }

        /// <summary>
        /// Gets or sets the continent.
        /// </summary>
        public virtual Continent Continent { get; set; }
    }
}
