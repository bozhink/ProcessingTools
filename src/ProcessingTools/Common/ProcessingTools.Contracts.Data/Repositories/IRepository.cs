// <copyright file="IRepository.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Data.Repositories
{
    using System.Threading.Tasks;

    /// <summary>
    /// Base repository
    /// </summary>
    public interface IRepository
    {
        /// <summary>
        /// Commit staged changes and save.
        /// </summary>
        /// <returns>Object</returns>
        object SaveChanges();

        /// <summary>
        /// Commit staged changes and save.
        /// </summary>
        /// <returns>Task</returns>
        Task<object> SaveChangesAsync();
    }
}
