﻿// <copyright file="ICitiesApiService.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

using System.Collections.Generic;
using System.Threading.Tasks;
using ProcessingTools.Web.Models.Geo.Cities;

namespace ProcessingTools.Contracts.Web.Services.Geo
{
    /// <summary>
    /// Web API service for management of city objects.
    /// </summary>
    public interface ICitiesApiService
    {
        /// <summary>
        /// Gets all city objects.
        /// </summary>
        /// <returns>Task of array of the response model.</returns>
        Task<IList<CityResponseModel>> GetAllAsync();

        /// <summary>
        /// Gets single city object by its ID.
        /// </summary>
        /// <param name="id">ID of the requested object.</param>
        /// <returns>Task of the response model.</returns>
        Task<CityResponseModel> GetById(int id);
    }
}
