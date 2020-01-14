﻿// <copyright file="ICoordinate2DParser.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Services.Geo.Coordinates
{
    using ProcessingTools.Contracts.Models.Geo.Coordinates;

    /// <summary>
    /// 2D coordinate parser.
    /// </summary>
    public interface ICoordinate2DParser
    {
        /// <summary>
        /// Parse coordinate string.
        /// </summary>
        /// <param name="coordinateString">Coordinate string to be parsed.</param>
        /// <param name="coordinateType">Type of the coordinate.</param>
        /// <param name="latitude">Latitude coordinate part to be updated.</param>
        /// <param name="longitude">Longitude coordinate part to be updated.</param>
        void ParseCoordinateString(string coordinateString, string coordinateType, ICoordinatePart latitude, ICoordinatePart longitude);
    }
}
