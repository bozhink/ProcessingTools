// <copyright file="IRepositoryAsync.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Contracts
{
    using System.Threading.Tasks;
    using ProcessingTools.Enumerations;
    using ProcessingTools.Models.Contracts;

    /// <summary>
    /// Generic repository.
    /// </summary>
    /// <typeparam name="TModel">Type of model.</typeparam>
    /// <typeparam name="TFilter">Type of filter.</typeparam>
    public interface IRepositoryAsync<TModel, TFilter>
        where TFilter : IFilter
    {
        /// <summary>
        /// Delete entity.
        /// </summary>
        /// <param name="model">Entity to be deleted.</param>
        /// <returns>Task of result.</returns>
        Task<object> DeleteAsync(TModel model);

        /// <summary>
        /// Delete entity by id;
        /// </summary>
        /// <param name="id">ID of the entity to be deleted.</param>
        /// <returns>Task of result.</returns>
        Task<object> DeleteAsync(object id);

        /// <summary>
        /// Get entity by id.
        /// </summary>
        /// <param name="id">ID of the entity.</param>
        /// <returns>Task of result.</returns>
        Task<TModel> GetByIdAsync(object id);

        /// <summary>
        /// Insert entity.
        /// </summary>
        /// <param name="model">Entity to be inserted.</param>
        /// <returns>Task of result.</returns>
        Task<object> InsertAsync(TModel model);

        /// <summary>
        /// Save changes.
        /// </summary>
        /// <returns>Task of result.</returns>
        Task<object> SaveChangesAsync();

        /// <summary>
        /// Select entities by filter.
        /// </summary>
        /// <param name="filter">Filter to be applied.</param>
        /// <returns>Task of selected entities.</returns>
        Task<TModel[]> SelectAsync(TFilter filter);

        /// <summary>
        /// Select entities by filter and order.
        /// </summary>
        /// <param name="filter">Filter to be applied.</param>
        /// <param name="skip">Number of items to be skipped.</param>
        /// <param name="take">Number of items to be taken.</param>
        /// <param name="sortColumn">Name of the column to sort.</param>
        /// <param name="sortOrder">Sort order.</param>
        /// <returns>Task of selected entities.</returns>
        Task<TModel[]> SelectAsync(TFilter filter, int skip, int take, string sortColumn, SortOrder sortOrder);

        /// <summary>
        /// Select the count of entities by filter.
        /// </summary>
        /// <param name="filter">Filter to be applied.</param>
        /// <returns>Task of number of entities.</returns>
        Task<long> SelectCountAsync(TFilter filter);

        /// <summary>
        /// Update entity.
        /// </summary>
        /// <param name="model">Entity to be updated.</param>
        /// <returns>Task of result.</returns>
        Task<object> UpdateAsync(TModel model);
    }
}
