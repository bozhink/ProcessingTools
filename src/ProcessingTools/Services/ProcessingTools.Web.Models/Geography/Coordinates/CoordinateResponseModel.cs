// <copyright file="CoordinateResponseModel.cs" company="ProcessingTools">
// Copyright (c) 2022 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Models.Geography.Coordinates
{
    /// <summary>
    /// Coordinate response model.
    /// </summary>
    public class CoordinateResponseModel
    {
        /// <summary>
        /// Gets or sets the coordinate string.
        /// </summary>
        public string Coordinate { get; set; }

        /// <summary>
        /// Gets or sets the latitude as string.
        /// </summary>
        public decimal? Latitude { get; set; }

        /// <summary>
        /// Gets or sets the longitude as string.
        /// </summary>
        public decimal? Longitude { get; set; }
    }
}
