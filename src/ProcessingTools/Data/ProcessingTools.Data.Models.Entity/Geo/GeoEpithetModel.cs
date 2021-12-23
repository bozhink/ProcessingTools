// <copyright file="GeoEpithetModel.cs" company="ProcessingTools">
// Copyright (c) 2022 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Models.Entity.Geo
{
    using ProcessingTools.Contracts.Models.Geo;

    /// <summary>
    /// Geo epithet model.
    /// </summary>
    public class GeoEpithetModel : IGeoEpithet
    {
        /// <summary>
        /// Gets or sets the ID of the geo epithet.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name value of the geo epithet.
        /// </summary>
        public string Name { get; set; }
    }
}
