// <copyright file="ICoordinate.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Processors.Models.Contracts.Geo.Coordinates
{
    /// <summary>
    /// Coordinate.
    /// </summary>
    public interface ICoordinate
    {
        /// <summary>
        /// Gets or sets latitude.
        /// </summary>
        string Latitude { get; set; }

        /// <summary>
        /// Gets or sets longitude.
        /// </summary>
        string Longitude { get; set; }
    }
}
