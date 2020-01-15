// <copyright file="ProvinceSynonym.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Models.Entity.Geo
{
    /// <summary>
    /// Province synonym.
    /// </summary>
    public class ProvinceSynonym : Synonym
    {
        /// <summary>
        /// Gets or sets the ID of the province.
        /// </summary>
        public virtual int ProvinceId { get; set; }

        /// <summary>
        /// Gets or sets the province.
        /// </summary>
        public virtual Province Province { get; set; }
    }
}
