// <copyright file="ICoordinatesParseService.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Services.Contracts.Geo.Coordinates
{
    using System.Threading.Tasks;
    using ProcessingTools.Web.Models.Tools.Coordinates;

    /// <summary>
    /// Coordinates parse service.
    /// </summary>
    public interface ICoordinatesParseService
    {
        /// <summary>
        /// Parses coordinates string.
        /// </summary>
        /// <param name="model">Request model.</param>
        /// <returns>Calculated coordinates.</returns>
        Task<CoordinatesResponseViewModel> ParseCoordinatesStringAsync(CoordinatesRequestModel model);
    }
}
