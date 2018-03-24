// <copyright file="IDeletableDataService{TModel}.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Contracts
{
    using System.Threading.Tasks;

    /// <summary>
    /// Generic deletable data service.
    /// </summary>
    /// <typeparam name="TModel">Type of the service model.</typeparam>
    public interface IDeletableDataService<TModel>
    {
        /// <summary>
        /// Deletes models.
        /// </summary>
        /// <param name="models">Models to be deleted.</param>
        /// <returns>Task</returns>
        Task<object> DeleteAsync(params TModel[] models);
    }
}
