// <copyright file="CoordinateStringModel.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

using ProcessingTools.Contracts.Services.Models.Geo.Coordinates;

namespace ProcessingTools.Services.Models.Geo.Coordinates
{
    using System;

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
