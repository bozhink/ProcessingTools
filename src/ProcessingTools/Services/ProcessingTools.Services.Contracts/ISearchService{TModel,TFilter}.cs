﻿// <copyright file="ISearchService{TModel,TFilter}.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Contracts
{
    using System.Threading.Tasks;

    /// <summary>
    /// Generic search service.
    /// </summary>
    /// <typeparam name="TModel">Type of model.</typeparam>
    /// <typeparam name="TFilter">Type of filter.</typeparam>
    public interface ISearchService<TModel, TFilter>
    {
        /// <summary>
        /// Do search with a specified filter.
        /// </summary>
        /// <param name="filter">Filter object for search.</param>
        /// <returns>Array of found objects.</returns>
        Task<TModel[]> SearchAsync(TFilter filter);
    }
}
