// <copyright file="IGeoCoordinateDeployModel.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Models.Contracts.Deployment.Geo
{
    /// <summary>
    /// Geo-coordinate deploy model.
    /// </summary>
    public interface IGeoCoordinateDeployModel : IDeployModel
    {
        /// <summary>
        /// Gets the decimal spherical value of the latitude.
        /// </summary>
        decimal Latitude { get; }

        /// <summary>
        /// Gets the decimal spherical value of the longitude.
        /// </summary>
        decimal Longitude { get; }

        /// <summary>
        /// Gets the value of the coordinate string as text.
        /// </summary>
        string CoordinateStringText { get; }

        /// <summary>
        /// Gets the value of the coordinate string as XML.
        /// </summary>
        string CoordinateStringXml { get; }
    }
}
