﻿// <copyright file="ICoordinatesParseService.cs" company="ProcessingTools">
// Copyright (c) 2021 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Services.Geo.Coordinates
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using ProcessingTools.Contracts.Models.Geo.Coordinates;

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
        Task<IList<ICoordinateStringModel>> ParseCoordinatesStringAsync(string coordinates);

        /// <summary>
        /// Parses coordinates as strings.
        /// </summary>
        /// <param name="coordinates">Coordinates as strings.</param>
        /// <returns>Array of calculated coordinate strings.</returns>
        Task<IList<ICoordinateStringModel>> ParseCoordinateStringsAsync(IEnumerable<string> coordinates);
    }
}
