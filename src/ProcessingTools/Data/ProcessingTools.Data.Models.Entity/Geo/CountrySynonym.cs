// <copyright file="CountrySynonym.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Models.Entity.Geo
{
    /// <summary>
    /// Country synonym.
    /// </summary>
    public class CountrySynonym : Synonym
    {
        /// <summary>
        /// Gets or sets the ID of the country.
        /// </summary>
        public virtual int CountryId { get; set; }

        /// <summary>
        /// Gets or sets the country.
        /// </summary>
        public virtual Country Country { get; set; }
    }
}
