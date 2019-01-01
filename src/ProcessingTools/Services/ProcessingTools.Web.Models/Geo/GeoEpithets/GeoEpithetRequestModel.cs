// <copyright file="GeoEpithetRequestModel.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Models.Geo.GeoEpithets
{
    using ProcessingTools.Models.Contracts.Geo;

    /// <summary>
    /// Geo epithet request model.
    /// </summary>
    public class GeoEpithetRequestModel : IGeoEpithet
    {
        /// <summary>
        /// Gets or sets the geo epithet ID.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the geo epithet name.
        /// </summary>
        public string Name { get; set; }
    }
}
