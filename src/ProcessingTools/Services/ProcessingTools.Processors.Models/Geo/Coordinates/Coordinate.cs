// <copyright file="Coordinate.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Processors.Models.Geo.Coordinates
{
    using ProcessingTools.Processors.Models.Contracts.Geo.Coordinates;

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
