// <copyright file="MunicipalitySynonym.cs" company="ProcessingTools">
// Copyright (c) 2021 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Models.Entity.Geo
{
    /// <summary>
    /// Municipality synonym.
    /// </summary>
    public class MunicipalitySynonym : Synonym
    {
        /// <summary>
        /// Gets or sets the ID of the municipality.
        /// </summary>
        public virtual int MunicipalityId { get; set; }

        /// <summary>
        /// Gets or sets the municipality.
        /// </summary>
        public virtual Municipality Municipality { get; set; }
    }
}
