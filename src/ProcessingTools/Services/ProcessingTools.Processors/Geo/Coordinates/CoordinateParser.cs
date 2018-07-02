// <copyright file="CoordinateParser.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Processors.Geo.Coordinates
{
    using System;
    using ProcessingTools.Enumerations;
    using ProcessingTools.Processors.Contracts.Geo.Coordinates;
    using ProcessingTools.Processors.Models.Contracts.Geo.Coordinates;

    /// <summary>
    /// Coordinate parser.
    /// </summary>
    public class CoordinateParser : ICoordinateParser
    {
        private readonly ICoordinate2DParser coordinate2DParser;
        private readonly ICoordinateFactory coordinateFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="CoordinateParser"/> class.
        /// </summary>
        /// <param name="coordinate2DParser">2D coordinate parser.</param>
        /// <param name="coordinateFactory">Coordinate factory.</param>
        public CoordinateParser(ICoordinate2DParser coordinate2DParser, ICoordinateFactory coordinateFactory)
        {
            this.coordinate2DParser = coordinate2DParser ?? throw new ArgumentNullException(nameof(coordinate2DParser));
            this.coordinateFactory = coordinateFactory ?? throw new ArgumentNullException(nameof(coordinateFactory));
        }

        /// <inheritdoc/>
        public ICoordinate ParseCoordinateString(string coordinateString) => this.ParseCoordinateString(coordinateString: coordinateString, coordinateType: null);

        /// <inheritdoc/>
        public ICoordinate ParseCoordinateString(string coordinateString, string coordinateType)
        {
            if (string.IsNullOrWhiteSpace(coordinateString))
            {
                throw new ArgumentNullException(nameof(coordinateString));
            }

            var latitude = this.coordinateFactory.CreateCoordinatePart();
            var longitude = this.coordinateFactory.CreateCoordinatePart();

            this.coordinate2DParser.ParseCoordinateString(coordinateString, coordinateType, latitude, longitude);

            var coordinate = this.coordinateFactory.CreateCoordinate();
            if (latitude.Type == CoordinatePartType.Longitude && longitude.Type == CoordinatePartType.Latitude)
            {
                coordinate.Latitude = longitude.PartIsPresent ? longitude.Value : null;
                coordinate.Longitude = latitude.PartIsPresent ? latitude.Value : null;
            }
            else
            {
                coordinate.Latitude = latitude.PartIsPresent ? latitude.Value : null;
                coordinate.Longitude = longitude.PartIsPresent ? longitude.Value : null;
            }

            return coordinate;
        }
    }
}
