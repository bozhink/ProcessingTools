// <copyright file="CoordinateStringModel.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Models.Geo.Coordinates
{
    using System;
    using ProcessingTools.Contracts.Models.Geo.Coordinates;

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

        /// <inheritdoc/>
        public Exception ParseException { get; set; }
    }
}
