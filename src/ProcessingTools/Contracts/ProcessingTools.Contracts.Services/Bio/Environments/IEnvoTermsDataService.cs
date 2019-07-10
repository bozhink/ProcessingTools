﻿// <copyright file="IEnvoTermsDataService.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

using System.Threading.Tasks;
using ProcessingTools.Contracts.Services.Models.Bio.Environments;

namespace ProcessingTools.Contracts.Services.Bio.Environments
{
    /// <summary>
    /// ENVO Terms data service.
    /// </summary>
    public interface IEnvoTermsDataService
    {
        /// <summary>
        /// Gets all ENVO terms.
        /// </summary>
        /// <returns>Array of ENVO terms.</returns>
        Task<IEnvoTerm[]> AllAsync();

        /// <summary>
        /// Gets ENVO terms.
        /// </summary>
        /// <param name="skip">Number of terms to skip.</param>
        /// <param name="take">Number of terms to take.</param>
        /// <returns>Array of ENVO terms.</returns>
        Task<IEnvoTerm[]> GetAsync(int skip, int take);
    }
}
