// <copyright file="IFileDbContext.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Common.File.Contracts
{
    using System.Linq;
    using System.Threading.Tasks;

    /// <summary>
    /// File DB context.
    /// </summary>
    /// <typeparam name="T">Type of file entity.</typeparam>
    public interface IFileDbContext<T>
    {
        /// <summary>
        /// Gets the data set.
        /// </summary>
        IQueryable<T> DataSet { get; }

        /// <summary>
        /// Inserts new entity into the DB context.
        /// </summary>
        /// <param name="entity">Entity to be inserted.</param>
        /// <returns>Task of result.</returns>
        Task<object> AddAsync(T entity);

        /// <summary>
        /// Deletes entity from the DB context.
        /// </summary>
        /// <param name="id">ID of the entity to be deleted.</param>
        /// <returns>Task of result.</returns>
        Task<object> DeleteAsync(object id);

        /// <summary>
        /// Gets entity by ID.
        /// </summary>
        /// <param name="id">ID of the entity.</param>
        /// <returns>Task of resultant entity.</returns>
        Task<T> GetAsync(object id);

        /// <summary>
        /// Updates entity in the DB context.
        /// </summary>
        /// <param name="entity">Entity to update.</param>
        /// <returns>Task of resultant object.</returns>
        Task<object> UpdateAsync(T entity);

        /// <summary>
        /// Loads data into the DB context from file.
        /// </summary>
        /// <param name="fileName">Name of the file to be loaded.</param>
        /// <returns>Task of number of loaded entities.</returns>
        Task<long> LoadFromFileAsync(string fileName);

        /// <summary>
        /// Writes entities from the context to file.
        /// </summary>
        /// <param name="fileName">Name of the file to be written.</param>
        /// <returns>Number of written entities.</returns>
        Task<long> WriteToFileAsync(string fileName);
    }
}
