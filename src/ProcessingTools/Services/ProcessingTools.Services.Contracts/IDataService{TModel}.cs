// <copyright file="IDataService{TModel}.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Contracts
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using ProcessingTools.Enumerations;
    using ProcessingTools.Models.Contracts;

    /// <summary>
    /// Generic data service.
    /// </summary>
    /// <typeparam name="TModel">Type of the service model.</typeparam>
    public interface IDataService<TModel>
        where TModel : IServiceModel
    {
        /// <summary>
        /// Adds new model.
        /// </summary>
        /// <param name="userId">User ID for validation.</param>
        /// <param name="model">Model to be added.</param>
        /// <returns>Task of result.</returns>
        Task<object> AddAsync(object userId, TModel model);

        /// <summary>
        /// Updates existing model.
        /// </summary>
        /// <param name="userId">User ID for validation.</param>
        /// <param name="model">Model to be updated.</param>
        /// <returns>Task of result.</returns>
        Task<object> UpdateAsync(object userId, TModel model);

        /// <summary>
        /// Deletes model by ID;
        /// </summary>
        /// <param name="userId">User ID for validation.</param>
        /// <param name="id">ID of the model to be deleted.</param>
        /// <returns>Task of result.</returns>
        Task<object> DeleteAsync(object userId, object id);

        /// <summary>
        /// Gets model by ID.
        /// </summary>
        /// <param name="userId">User ID for validation.</param>
        /// <param name="id">ID of the model to be retrieved.</param>
        /// <returns>The model.</returns>
        Task<TModel> GetAsync(object userId, object id);

        /// <summary>
        /// Gets paged and sorted data.
        /// </summary>
        /// <param name="userId">User ID for validation.</param>
        /// <param name="skip">Number of items to be skipped.</param>
        /// <param name="take">Number of items to be taken.</param>
        /// <param name="sort">Sorting expression.</param>
        /// <param name="order"><see cref="SortOrder"/> for sorting.</param>
        /// <param name="filter">Filter expression.</param>
        /// <returns>Pages and sorted models.</returns>
        Task<IEnumerable<TModel>> SelectAsync(object userId, int skip, int take, Expression<Func<TModel, object>> sort, SortOrder order = SortOrder.Ascending, Expression<Func<TModel, bool>> filter = null);
    }
}
