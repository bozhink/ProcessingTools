﻿// <copyright file="ICoordinate.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Services.Models.Geo.Coordinates
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
