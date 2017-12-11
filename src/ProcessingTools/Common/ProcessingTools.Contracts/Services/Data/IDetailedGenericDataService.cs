// <copyright file="IDetailedGenericDataService.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using ProcessingTools.Contracts.Models;
    using ProcessingTools.Enumerations;

    /// <summary>
    /// Data service with details.
    /// </summary>
    /// <typeparam name="TModel">Type of the service model.</typeparam>
    /// <typeparam name="TDetailedModel">Type of the detailed service model.</typeparam>
    public interface IDetailedGenericDataService<TModel, TDetailedModel> : IDataService<TModel>
        where TModel : IServiceModel
        where TDetailedModel : TModel, IDetailedModel
    {
        /// <summary>
        /// Gets details of a specified model.
        /// </summary>
        /// <param name="userId">User ID for validation.</param>
        /// <param name="id">ID of the model to be retrieved.</param>
        /// <returns>Detailed model.</returns>
        Task<TDetailedModel> GetDetailsAsync(object userId, object id);

        /// <summary>
        ///  Gets paged and sorted details.
        /// </summary>
        /// <param name="userId">User ID for validation.</param>
        /// <param name="skip">Number of items to be skipped.</param>
        /// <param name="take">Number of items to be taken.</param>
        /// <param name="sort">Sorting expression.</param>
        /// <param name="order"><see cref="SortOrder"/> for sorting.</param>
        /// <param name="filter">Filter expression.</param>
        /// <returns>Pages and sorted detailed models.</returns>
        Task<IEnumerable<TDetailedModel>> SelectDetailsAsync(object userId, int skip, int take, Expression<Func<TDetailedModel, object>> sort, SortOrder order = SortOrder.Ascending, Expression<Func<TDetailedModel, bool>> filter = null);
    }
}
