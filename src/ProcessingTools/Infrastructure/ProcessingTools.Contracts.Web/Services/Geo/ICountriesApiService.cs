﻿// <copyright file="ICountriesApiService.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Web.Services.Geo
{
    using System.Threading.Tasks;
    using ProcessingTools.Web.Models.Geo.Countries;

    /// <summary>
    /// Web API service for management of country objects.
    /// </summary>
    public interface ICountriesApiService
    {
        /// <summary>
        /// Gets all country objects.
        /// </summary>
        /// <returns>Task of array of the response model</returns>
        Task<CountryResponseModel[]> GetAllAsync();
    }
}
