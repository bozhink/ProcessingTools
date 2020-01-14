// <copyright file="IEnvoTermsDataService.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Services.Bio.Environments
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using ProcessingTools.Contracts.Models.Bio.Environments;

    /// <summary>
    /// ENVO Terms data service.
    /// </summary>
    public interface IEnvoTermsDataService
    {
        /// <summary>
        /// Gets all ENVO terms.
        /// </summary>
        /// <returns>Array of ENVO terms.</returns>
        Task<IList<IEnvoTerm>> AllAsync();

        /// <summary>
        /// Gets ENVO terms.
        /// </summary>
        /// <param name="skip">Number of terms to skip.</param>
        /// <param name="take">Number of terms to take.</param>
        /// <returns>Array of ENVO terms.</returns>
        Task<IList<IEnvoTerm>> GetAsync(int skip, int take);
    }
}
