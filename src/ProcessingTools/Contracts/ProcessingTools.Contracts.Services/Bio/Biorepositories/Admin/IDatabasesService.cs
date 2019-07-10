// <copyright file="IDatabasesService.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

using System.Threading.Tasks;
using ProcessingTools.Contracts.Services.Models.Admin.Databases;

namespace ProcessingTools.Contracts.Services.Bio.Biorepositories.Admin
{
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
