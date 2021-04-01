// <copyright file="CitySynonym.cs" company="ProcessingTools">
// Copyright (c) 2021 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Models.Entity.Geo
{
    /// <summary>
    /// City synonym.
    /// </summary>
    public class CitySynonym : Synonym
    {
        /// <summary>
        /// Gets or sets the ID of the city.
        /// </summary>
        public virtual int CityId { get; set; }

        /// <summary>
        /// Gets or sets the city.
        /// </summary>
        public virtual City City { get; set; }
    }
}
