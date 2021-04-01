// <copyright file="Coordinate.cs" company="ProcessingTools">
// Copyright (c) 2021 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Models.Geo.Coordinates
{
    using ProcessingTools.Contracts.Models.Geo.Coordinates;

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
