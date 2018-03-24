// <copyright file="ISearchableRepository{T}.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Contracts
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    /// <summary>
    /// Generic searchable repository.
    /// </summary>
    /// <typeparam name="T">Type of the entity.</typeparam>
    public interface ISearchableRepository<T> : IRepository<T>
    {
        /// <summary>
        /// Gets query.
        /// </summary>
        IQueryable<T> Query { get; }

        /// <summary>
        /// Gets entity by ID.
        /// </summary>
        /// <param name="id">ID of the entity.</param>
        /// <returns>Task of entity.</returns>
        Task<T> GetByIdAsync(object id);

        /// <summary>
        /// Finds first occurrence of entity by filter.
        /// </summary>
        /// <param name="filter">Filter to be applied.</param>
        /// <returns>Task of entity.</returns>
        Task<T> FindFirstAsync(Expression<Func<T, bool>> filter);

        /// <summary>
        /// Find all occurrences by filter.
        /// </summary>
        /// <param name="filter">Filter to be applied.</param>
        /// <returns>Task of found entities.</returns>
        Task<T[]> FindAsync(Expression<Func<T, bool>> filter);
    }
}
