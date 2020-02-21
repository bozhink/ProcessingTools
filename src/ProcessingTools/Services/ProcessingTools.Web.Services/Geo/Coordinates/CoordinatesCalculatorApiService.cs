// <copyright file="CoordinatesCalculatorApiService.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Services.Geo.Coordinates
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Logging;
    using ProcessingTools.Contracts.Services.Geo.Coordinates;
    using ProcessingTools.Contracts.Web.Services.Geo.Coordinates;
    using ProcessingTools.Extensions.Text;
    using ProcessingTools.Web.Models.Geography.Coordinates;

    /// <summary>
    /// Coordinates calculator API service.
    /// </summary>
    public class CoordinatesCalculatorApiService : ICoordinatesCalculatorApiService
    {
        private readonly ICoordinateParser coordinateParser;
        private readonly ILogger logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="CoordinatesCalculatorApiService"/> class.
        /// </summary>
        /// <param name="coordinateParser">Instance of <see cref="ICoordinateParser"/> to parse coordinates.</param>
        /// <param name="logger">Logger.</param>
        public CoordinatesCalculatorApiService(ICoordinateParser coordinateParser, ILogger<CoordinatesCalculatorApiService> logger)
        {
            this.coordinateParser = coordinateParser ?? throw new ArgumentNullException(nameof(coordinateParser));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <inheritdoc/>
        public Task<IList<CoordinateResponseModel>> ParseCoordinatesAsync(string coordinates)
        {
            if (string.IsNullOrWhiteSpace(coordinates))
            {
                return Task.FromResult<IList<CoordinateResponseModel>>(Array.Empty<CoordinateResponseModel>());
            }

            return this.ParseCoordinatesInternalAsync(coordinates.GetNonEmptyLines());
        }

        /// <inheritdoc/>
        public Task<IList<CoordinateResponseModel>> ParseCoordinatesAsync(IList<string> coordinates)
        {
            if (coordinates is null || !coordinates.Any())
            {
                return Task.FromResult<IList<CoordinateResponseModel>>(Array.Empty<CoordinateResponseModel>());
            }

            return this.ParseCoordinatesInternalAsync(coordinates);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1031:Do not catch general exception types", Justification = "Not processed items will be returned as default")]
        private async Task<IList<CoordinateResponseModel>> ParseCoordinatesInternalAsync(IList<string> coordinates)
        {
            var cleanedCoordinates = new HashSet<string>(coordinates.Select(s => s.Trim()).Where(s => !string.IsNullOrWhiteSpace(s)));

            IList<CoordinateResponseModel> result = new List<CoordinateResponseModel>();

            foreach (var coordinateString in coordinates)
            {
                try
                {
                    var coordinate = new CoordinateResponseModel
                    {
                        Coordinate = coordinateString,
                    };

                    if (cleanedCoordinates.Contains(coordinateString))
                    {
                        var parsedCoordinate = this.coordinateParser.ParseCoordinateString(coordinateString);

                        if (decimal.TryParse(parsedCoordinate?.Latitude, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal latitude))
                        {
                            coordinate.Latitude = latitude;
                        }

                        if (decimal.TryParse(parsedCoordinate?.Longitude, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal longitude))
                        {
                            coordinate.Longitude = longitude;
                        }
                    }

                    result.Add(coordinate);
                }
                catch (Exception ex)
                {
                    this.logger.LogError(ex, string.Empty);
                }
            }

            await Task.CompletedTask.ConfigureAwait(false);

            return result;
        }
    }
}
