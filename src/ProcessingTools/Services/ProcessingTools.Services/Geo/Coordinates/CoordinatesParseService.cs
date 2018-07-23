// <copyright file="CoordinatesParseService.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Geo.Coordinates
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using ProcessingTools.Processors.Contracts.Geo.Coordinates;
    using ProcessingTools.Services.Contracts.Geo.Coordinates;
    using ProcessingTools.Services.Models.Contracts.Geo.Coordinates;
    using ProcessingTools.Services.Models.Geo.Coordinates;

    /// <summary>
    /// Coordinates parse service.
    /// </summary>
    public class CoordinatesParseService : ICoordinatesParseService
    {
        private readonly ICoordinateParser coordinateParser;

        /// <summary>
        /// Initializes a new instance of the <see cref="CoordinatesParseService"/> class.
        /// </summary>
        /// <param name="coordinateParser">Coordinate parser.</param>
        public CoordinatesParseService(ICoordinateParser coordinateParser)
        {
            this.coordinateParser = coordinateParser ?? throw new ArgumentNullException(nameof(coordinateParser));
        }

        /// <inheritdoc/>
        public Task<ICoordinateStringModel[]> ParseCoordinatesStringAsync(string coordinates)
        {
            return Task.Run(() =>
            {
                if (string.IsNullOrWhiteSpace(coordinates))
                {
                    return Array.Empty<ICoordinateStringModel>();
                }

                return coordinates.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(c => c.Trim())
                    .Where(c => c.Length > 1)
                    .Distinct()
                    .Select(c =>
                    {
                        try
                        {
                            var coordinate = this.coordinateParser.ParseCoordinateString(c);
                            return new CoordinateStringModel
                            {
                                Coordinate = c,
                                Latitude = coordinate?.Latitude,
                                Longitude = coordinate?.Longitude,
                            };
                        }
                        catch
                        {
                            return new CoordinateStringModel
                            {
                                Coordinate = c
                            };
                        }
                    })
                    .ToArray();
            });
        }
    }
}
