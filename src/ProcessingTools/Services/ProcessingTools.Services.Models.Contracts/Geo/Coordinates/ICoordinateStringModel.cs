// <copyright file="ICoordinateStringModel.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Models.Contracts.Geo.Coordinates
{
    /// <summary>
    /// Coordinate string model.
    /// </summary>
    public interface ICoordinateStringModel
    {
        /// <summary>
        /// Gets the coordinate string.
        /// </summary>
        string Coordinate { get; }

        /// <summary>
        /// Gets the latitude as string.
        /// </summary>
        string Latitude { get; }

        /// <summary>
        /// Gets the longitude as string.
        /// </summary>
        string Longitude { get; }
    }
}
