// <copyright file="IFilesRepository.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Contracts.Documents
{
    using System.Threading.Tasks;
    using ProcessingTools.Models.Contracts.Documents;

    /// <summary>
    /// Files repository.
    /// </summary>
    public interface IFilesRepository : IRepository
    {
        /// <summary>
        /// Adds a file.
        /// </summary>
        /// <param name="entity">File to be added.</param>
        /// <returns>Task</returns>
        Task<object> AddAsync(IFile entity);

        /// <summary>
        /// Gets file by ID.
        /// </summary>
        /// <param name="id">ID of the file.</param>
        /// <returns>Task of <see cref="IFile"/>.</returns>
        Task<IFile> GetAsync(object id);

        /// <summary>
        /// Removes file by ID.
        /// </summary>
        /// <param name="id">ID of the file to be removed.</param>
        /// <returns>Task</returns>
        Task<object> RemoveAsync(object id);

        /// <summary>
        /// Update a file.
        /// </summary>
        /// <param name="entity">File to be updated.</param>
        /// <returns>Task</returns>
        Task<object> UpdateAsync(IFile entity);
    }
}
