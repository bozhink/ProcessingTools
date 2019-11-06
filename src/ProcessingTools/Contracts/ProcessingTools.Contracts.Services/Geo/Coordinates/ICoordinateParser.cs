﻿// <copyright file="ICoordinateParser.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Services.Geo.Coordinates
{
    using ProcessingTools.Contracts.Models.Geo.Coordinates;

    /// <summary>
    /// Coordinate parser.
    /// </summary>
    public interface ICoordinateParser
    {
        /// <summary>
        /// Parse coordinate string.
        /// </summary>
        /// <param name="coordinateString">Coordinate string to be parsed.</param>
        /// <returns>Parsed coordinate.</returns>
        ICoordinate ParseCoordinateString(string coordinateString);

        /// <summary>
        /// Parse coordinate string with specified type.
        /// </summary>
        /// <param name="coordinateString">Coordinate string to be parsed.</param>
        /// <param name="coordinateType">Type of the coordinate.</param>
        /// <returns>Parsed coordinate.</returns>
        ICoordinate ParseCoordinateString(string coordinateString, string coordinateType);
    }
}