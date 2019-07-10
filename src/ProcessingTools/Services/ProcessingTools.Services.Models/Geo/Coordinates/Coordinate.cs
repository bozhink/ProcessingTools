// <copyright file="Coordinate.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

using ProcessingTools.Contracts.Services.Models.Geo.Coordinates;

namespace ProcessingTools.Services.Models.Geo.Coordinates
{
    /// <summary>
    /// Coordinate.
    /// </summary>
    public class Coordinate : ICoordinate
    {
        /// <inheritdoc/>
        public string Latitude { get; set; }

        /// <inheritdoc/>
        public string Longitude { get; set; }
    }
}
