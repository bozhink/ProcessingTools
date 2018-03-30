// <copyright file="IDatabasesService.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Contracts.Admin
{
    using System.Threading.Tasks;
    using ProcessingTools.Services.Models.Contracts.Admin.Databases;

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
