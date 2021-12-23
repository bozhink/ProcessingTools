// <copyright file="ICountriesApiService.cs" company="ProcessingTools">
// Copyright (c) 2022 ProcessingTools. All rights reserved.
// </copyright>

using System.Collections.Generic;
using System.Threading.Tasks;
using ProcessingTools.Web.Models.Geo.Countries;

namespace ProcessingTools.Contracts.Web.Services.Geo
{
    /// <summary>
    /// Web API service for management of country objects.
    /// </summary>
    public interface ICountriesApiService
    {
        /// <summary>
        /// Gets all country objects.
        /// </summary>
        /// <returns>Task of array of the response model.</returns>
        Task<IList<CountryResponseModel>> GetAllAsync();
    }
}
