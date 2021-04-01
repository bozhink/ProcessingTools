﻿// <copyright file="CoordinateFactory.cs" company="ProcessingTools">
// Copyright (c) 2021 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Geo.Coordinates
{
    using System;
    using ProcessingTools.Contracts.Models.Geo.Coordinates;
    using ProcessingTools.Contracts.Services.Geo.Coordinates;

    /// <summary>
    /// Coordinate factory.
    /// </summary>
    public class CoordinateFactory : ICoordinateFactory
    {
        private readonly Func<ICoordinate> coordinateFactory;
        private readonly Func<ICoordinatePart> coordinatePartFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="CoordinateFactory"/> class.
        /// </summary>
        /// <param name="coordinateFactory"><see cref="ICoordinate"/> factory.</param>
        /// <param name="coordinatePartFactory"><see cref="ICoordinatePart"/> factory.</param>
        public CoordinateFactory(Func<ICoordinate> coordinateFactory, Func<ICoordinatePart> coordinatePartFactory)
        {
            this.coordinateFactory = coordinateFactory ?? throw new ArgumentNullException(nameof(coordinateFactory));
            this.coordinatePartFactory = coordinatePartFactory ?? throw new ArgumentNullException(nameof(coordinatePartFactory));
        }

        /// <inheritdoc/>
        public ICoordinate CreateCoordinate() => this.coordinateFactory.Invoke();

        /// <inheritdoc/>
        public ICoordinatePart CreateCoordinatePart() => this.coordinatePartFactory.Invoke();
    }
}
