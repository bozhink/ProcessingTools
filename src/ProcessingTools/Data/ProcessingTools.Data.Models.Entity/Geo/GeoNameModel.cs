// <copyright file="GeoNameModel.cs" company="ProcessingTools">
// Copyright (c) 2021 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Models.Entity.Geo
{
    using ProcessingTools.Contracts.Models.Geo;

    /// <summary>
    /// Geo name model.
    /// </summary>
    public class GeoNameModel : IGeoName
    {
        /// <summary>
        /// Gets or sets the ID of the geo name.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name value of the geo name.
        /// </summary>
        public string Name { get; set; }
    }
}
