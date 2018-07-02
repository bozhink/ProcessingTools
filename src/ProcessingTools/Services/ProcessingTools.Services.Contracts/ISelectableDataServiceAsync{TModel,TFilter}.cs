// <copyright file="ISelectableDataServiceAsync{TModel,TFilter}.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Contracts
{
    using System.Threading.Tasks;
    using ProcessingTools.Enumerations;
    using ProcessingTools.Models.Contracts;

    /// <summary>
    /// Generic selectable data service.
    /// </summary>
    /// <typeparam name="TModel">Type of the service model.</typeparam>
    /// <typeparam name="TFilter">Type of the search filter.</typeparam>
    public interface ISelectableDataServiceAsync<TModel, TFilter>
        where TFilter : IFilter
    {
        /// <summary>
        /// Gets model by ID.
        /// </summary>
        /// <param name="id">ID of the model.</param>
        /// <returns>Task of resultant model.</returns>
        Task<TModel> GetByIdAsync(object id);

        /// <summary>
        /// Gets models by filter.
        /// </summary>
        /// <param name="filter">Filter for selection.</param>
        /// <returns>Task of resultant model's array.</returns>
        Task<TModel[]> SelectAsync(TFilter filter);

        /// <summary>
        /// Gets models by search parameters.
        /// </summary>
        /// <param name="filter">Filter for selection.</param>
        /// <param name="skip">Number of items to skip.</param>
        /// <param name="take">Number of items to take.</param>
        /// <param name="sortColumn">Name of the column to sort.</param>
        /// <param name="sortOrder">Sort order.</param>
        /// <returns>Task of resultant model's array.</returns>
        Task<TModel[]> SelectAsync(TFilter filter, int skip, int take, string sortColumn, SortOrder sortOrder);

        /// <summary>
        /// Gets the count of models by filter.
        /// </summary>
        /// <param name="filter">Filter for selection.</param>
        /// <returns>Task of number of items.</returns>
        Task<long> SelectCountAsync(TFilter filter);
    }
}
