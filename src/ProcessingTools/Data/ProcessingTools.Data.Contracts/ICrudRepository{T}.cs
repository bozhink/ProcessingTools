// <copyright file="ICrudRepository{T}.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Contracts
{
    using System.Threading.Tasks;
    using ProcessingTools.Contracts.Data.Expressions;

    /// <summary>
    /// Generic CRUD Repository
    /// </summary>
    /// <typeparam name="T">Type of entity</typeparam>
    public interface ICrudRepository<T> : ISearchableRepository<T>
    {
        /// <summary>
        /// Adds entity.
        /// </summary>
        /// <param name="entity">Entity to be added.</param>
        /// <returns>Task</returns>
        Task<object> AddAsync(T entity);

        /// <summary>
        /// Deletes entity by ID.
        /// </summary>
        /// <param name="id">ID of the entity to be deleted.</param>
        /// <returns>Task</returns>
        Task<object> DeleteAsync(object id);

        /// <summary>
        /// Updates entity.
        /// </summary>
        /// <param name="entity">Entity to be updated.</param>
        /// <returns>Task</returns>
        Task<object> UpdateAsync(T entity);

        /// <summary>
        /// Updated entity.
        /// </summary>
        /// <param name="id">ID of the entity to be updated.</param>
        /// <param name="updateExpression">Update expression.</param>
        /// <returns>Task</returns>
        Task<object> UpdateAsync(object id, IUpdateExpression<T> updateExpression);
    }
}
