// <copyright file="ICrudRepository{T}.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Contracts
{
    using System.Linq;
    using System.Threading.Tasks;

    /// <summary>
    /// Generic CRUD Repository.
    /// </summary>
    /// <typeparam name="T">Type of entity.</typeparam>
    public interface ICrudRepository<T> : IRepository<T>
    {
        /// <summary>
        /// Gets query.
        /// </summary>
        IQueryable<T> Query { get; }

        /// <summary>
        /// Adds entity.
        /// </summary>
        /// <param name="entity">Entity to be added.</param>
        /// <returns>Task.</returns>
        Task<object> AddAsync(T entity);
    }
}
