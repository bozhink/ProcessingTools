// <copyright file="ICoordinatesParseService.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

using System.Collections.Generic;
using System.Threading.Tasks;
using ProcessingTools.Contracts.Services.Models.Geo.Coordinates;

namespace ProcessingTools.Contracts.Services.Geo.Coordinates
{
    /// <summary>
    /// Coordinates parse service.
    /// </summary>
    public interface ICoordinatesParseService
    {
        /// <summary>
        /// Parses coordinates string. Each coordinate is supposed to be on separate row.
        /// </summary>
        /// <param name="coordinates">Coordinates string.</param>
        /// <returns>Array of calculated coordinate strings.</returns>
        Task<ICoordinateStringModel[]> ParseCoordinatesStringAsync(string coordinates);

        /// <summary>
        /// Parses coordinates as strings.
        /// </summary>
        /// <param name="coordinates">Coordinates as strings.</param>
        /// <returns>Array of calculated coordinate strings.</returns>
        Task<ICoordinateStringModel[]> ParseCoordinateStringsAsync(IEnumerable<string> coordinates);
    }
}
