// <copyright file="ICoordinateStringModel.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

using System;

namespace ProcessingTools.Contracts.Services.Models.Geo.Coordinates
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

        /// <summary>
        /// Gets the exception raised during parsing process.
        /// </summary>
        Exception ParseException { get; }
    }
}
