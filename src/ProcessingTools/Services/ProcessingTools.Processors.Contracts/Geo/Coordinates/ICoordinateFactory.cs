// <copyright file="ICoordinateFactory.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Processors.Contracts.Geo.Coordinates
{
    using ProcessingTools.Processors.Models.Contracts.Geo.Coordinates;

    /// <summary>
    /// Coordinates factory.
    /// </summary>
    public interface ICoordinateFactory
    {
        /// <summary>
        /// Creates new instance of <see cref="ICoordinate"/>.
        /// </summary>
        /// <returns>New instance of <see cref="ICoordinate"/>.</returns>
        ICoordinate CreateCoordinate();

        /// <summary>
        /// Creates new instance of <see cref="ICoordinatePart"/>.
        /// </summary>
        /// <returns>New instance of <see cref="ICoordinatePart"/>.</returns>
        ICoordinatePart CreateCoordinatePart();
    }
}
