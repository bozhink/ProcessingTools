﻿// <copyright file="IDataServiceAsync{TModel,TFilter}.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

using System.Threading.Tasks;
using ProcessingTools.Contracts.Models;

namespace ProcessingTools.Contracts.Services
{
    /// <summary>
    /// Generic data service.
    /// </summary>
    /// <typeparam name="TModel">Type of the service model.</typeparam>
    /// <typeparam name="TFilter">Type of the search filter.</typeparam>
    public interface IDataServiceAsync<TModel, TFilter> : ISelectableDataServiceAsync<TModel, TFilter>
        where TFilter : IFilter
    {
        /// <summary>
        /// Deleted model.
        /// </summary>
        /// <param name="model">Model to be deleted.</param>
        /// <returns>Task.</returns>
        Task<object> DeleteAsync(TModel model);

        /// <summary>
        /// Deletes model by ID.
        /// </summary>
        /// <param name="id">ID of the model to be deleted.</param>
        /// <returns>Task.</returns>
        Task<object> DeleteByIdAsync(object id);

        /// <summary>
        /// Inserts model.
        /// </summary>
        /// <param name="model">Model to be inserted.</param>
        /// <returns>Task.</returns>
        Task<object> InsertAsync(TModel model);

        /// <summary>
        /// Updates model.
        /// </summary>
        /// <param name="model">Model to be updated.</param>
        /// <returns>Task.</returns>
        Task<object> UpdateAsync(TModel model);
    }
}
