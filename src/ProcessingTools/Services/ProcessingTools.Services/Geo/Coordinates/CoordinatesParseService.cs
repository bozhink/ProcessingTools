// <copyright file="CoordinatesParseService.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Geo.Coordinates
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
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
        public async Task<ICoordinateStringModel[]> ParseCoordinatesStringAsync(string coordinates)
        {
            if (string.IsNullOrWhiteSpace(coordinates))
            {
                return Array.Empty<ICoordinateStringModel>();
            }

            var query = coordinates.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(c => c.Trim())
                .Where(c => c.Length > 1);

            var coordinatesCollection = new HashSet<string>(query);

            return await this.ParseCoordinateStringsAsync(coordinatesCollection).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public Task<ICoordinateStringModel[]> ParseCoordinateStringsAsync(IEnumerable<string> coordinates)
        {
            return Task.Run(() =>
            {
                if (coordinates == null || !coordinates.Any())
                {
                    return Array.Empty<ICoordinateStringModel>();
                }

                var resultDictionary = new ConcurrentDictionary<string, ICoordinateStringModel>();

                foreach (var coordinateString in coordinates)
                {
                    resultDictionary.GetOrAdd(coordinateString, key =>
                    {
                        try
                        {
                            var coordinate = this.coordinateParser.ParseCoordinateString(key);
                            return new CoordinateStringModel
                            {
                                Coordinate = key,
                                Latitude = coordinate?.Latitude,
                                Longitude = coordinate?.Longitude,
                            };
                        }
                        catch (Exception ex)
                        {
                            return new CoordinateStringModel
                            {
                                Coordinate = key,
                                ParseException = ex
                            };
                        }
                    });
                }

                return resultDictionary.Values.ToArray();
            });
        }
    }
}
