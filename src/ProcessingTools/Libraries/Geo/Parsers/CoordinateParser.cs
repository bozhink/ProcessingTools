namespace ProcessingTools.Geo.Parsers
{
    using System;
    using Contracts.Factories;
    using Contracts.Models;
    using Contracts.Parsers;
    using Types;

    public class CoordinateParser : ICoordinateParser
    {
        private readonly ICoordinate2DParser coordinate2DParser;
        private readonly ICoordinatesFactory coordinatesFactory;

        public CoordinateParser(ICoordinate2DParser coordinate2DParser, ICoordinatesFactory coordinatesFactory)
        {
            if (coordinate2DParser == null)
            {
                throw new ArgumentNullException(nameof(coordinate2DParser));
            }

            if (coordinatesFactory == null)
            {
                throw new ArgumentNullException(nameof(coordinatesFactory));
            }

            this.coordinate2DParser = coordinate2DParser;
            this.coordinatesFactory = coordinatesFactory;
        }

        public ICoordinate ParseCoordinateString(string coordinateString, string coordinateType = null)
        {
            if (string.IsNullOrWhiteSpace(coordinateString))
            {
                throw new ArgumentNullException(nameof(coordinateString));
            }

            var latitude = this.coordinatesFactory.CreateCoordinatePart();
            var longitude = this.coordinatesFactory.CreateCoordinatePart();

            this.coordinate2DParser.ParseCoordinateString(coordinateString, coordinateType, latitude, longitude);

            var coordinate = this.coordinatesFactory.CreateCoordinate();
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
