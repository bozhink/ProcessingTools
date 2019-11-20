// <copyright file="ICoordinatesCalculatorApiService.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Web.Services.Geo.Coordinates
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using ProcessingTools.Web.Models.Geography.Coordinates;

    /// <summary>
    /// Coordinates calculator API service.
    /// </summary>
    public interface ICoordinatesCalculatorApiService
    {
        /// <summary>
        /// Parse list of coordinates provided as single string.
        /// </summary>
        /// <param name="coordinates">List of coordinates as string.</param>
        /// <returns>List of parsed coordinates.</returns>
        Task<IList<CoordinateResponseModel>> ParseCoordinatesAsync(string coordinates);

        /// <summary>
        /// Parse list of coordinate strings.
        /// </summary>
        /// <param name="coordinates">Coordinates as list of strings</param>
        /// <returns>List of parsed coordinates</returns>
        Task<IList<CoordinateResponseModel>> ParseCoordinatesAsync(IList<string> coordinates);
    }
}
