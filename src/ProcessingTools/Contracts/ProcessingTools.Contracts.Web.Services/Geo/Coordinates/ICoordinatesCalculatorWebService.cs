﻿// <copyright file="ICoordinatesCalculatorWebService.cs" company="ProcessingTools">
// Copyright (c) 2021 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Web.Services.Geo.Coordinates
{
    using System.Threading.Tasks;
    using ProcessingTools.Web.Models.Tools.Coordinates;

    /// <summary>
    /// Coordinates calculator web presenter.
    /// </summary>
    public interface ICoordinatesCalculatorWebService : IWebPresenter
    {
        /// <summary>
        /// Get <see cref="CoordinatesViewModel"/>.
        /// </summary>
        /// <returns>Task of <see cref="CoordinatesViewModel"/>.</returns>
        Task<CoordinatesViewModel> GetCoordinatesViewModelAsync();

        /// <summary>
        /// Parse coordinates string.
        /// </summary>
        /// <param name="model">Request model.</param>
        /// <returns>Calculated coordinates.</returns>
        Task<CoordinatesViewModel> ParseCoordinatesAsync(CoordinatesRequestModel model);
    }
}
