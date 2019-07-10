// <copyright file="IBiorepositoriesDataService{T}.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

using System.Threading.Tasks;

namespace ProcessingTools.Contracts.Services.Bio.Biorepositories
{
    /// <summary>
    /// Generic biorepositories data service.
    /// </summary>
    /// <typeparam name="T">Type of service model.</typeparam>
    public interface IBiorepositoriesDataService<T>
        where T : class
    {
        /// <summary>
        /// Gets items.
        /// </summary>
        /// <param name="skip">Number of items to skip.</param>
        /// <param name="take">Number of items to take.</param>
        /// <returns>Array of items as specified service model.</returns>
        Task<T[]> GetAsync(int skip, int take);
    }
}
