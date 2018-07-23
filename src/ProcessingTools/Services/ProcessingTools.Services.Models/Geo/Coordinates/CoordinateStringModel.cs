// <copyright file="CoordinateStringModel.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Models.Geo.Coordinates
{
    using ProcessingTools.Services.Models.Contracts.Geo.Coordinates;

    /// <summary>
    /// Coordinate string model.
    /// </summary>
    public class CoordinateStringModel : ICoordinateStringModel
    {
        /// <inheritdoc/>
        public string Coordinate { get; set; }

        /// <inheritdoc/>
        public string Latitude { get; set; }

        /// <inheritdoc/>
        public string Longitude { get; set; }
    }
}
