// <copyright file="IMultiDataServiceAsync.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Services.Data
{
    using System.Threading.Tasks;
    using ProcessingTools.Models.Contracts;

    /// <summary>
    /// Data service with multiple model operations.
    /// </summary>
    /// <typeparam name="TModel">Type of the service model.</typeparam>
    /// <typeparam name="TFilter">Type of the filter.</typeparam>
    public interface IMultiDataServiceAsync<TModel, TFilter> : ISelectableDataServiceAsync<TModel, TFilter>
        where TFilter : IFilter
    {
        /// <summary>
        /// Deletes multiple models.
        /// </summary>
        /// <param name="models">Models to be deleted.</param>
        /// <returns>Task of result.</returns>
        Task<object> DeleteAsync(params TModel[] models);

        /// <summary>
        /// Deletes multiple models by their ID-s.
        /// </summary>
        /// <param name="ids">ID-s of the models to be deleted.</param>
        /// <returns>Task of result.</returns>
        Task<object> DeleteAsync(params object[] ids);

        /// <summary>
        /// Inserts multiple models.
        /// </summary>
        /// <param name="models">Models to be inserted.</param>
        /// <returns>Task of result.</returns>
        Task<object> InsertAsync(params TModel[] models);

        /// <summary>
        /// Updates multiple models.
        /// </summary>
        /// <param name="models">Models to be updated.</param>
        /// <returns>Task of result.</returns>
        Task<object> UpdateAsync(params TModel[] models);
    }
}
