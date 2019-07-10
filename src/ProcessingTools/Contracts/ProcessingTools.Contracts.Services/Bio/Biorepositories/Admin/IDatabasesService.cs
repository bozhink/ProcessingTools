// <copyright file="IDatabasesService.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Services.Bio.Biorepositories.Admin
{
    using System.Threading.Tasks;
    using ProcessingTools.Contracts.Services.Models.Admin.Databases;

    /// <summary>
    /// Databases service.
    /// </summary>
    public interface IDatabasesService
    {
        /// <summary>
        /// Initialize all databases.
        /// </summary>
        /// <returns>Result response model.</returns>
        Task<IInitializeModel> InitializeAllAsync();
    }
}
