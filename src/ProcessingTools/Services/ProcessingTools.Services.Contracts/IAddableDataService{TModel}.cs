﻿// <copyright file="IAddableDataService{TModel}.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Contracts
{
    using System.Threading.Tasks;

    /// <summary>
    /// Generic addable data service.
    /// </summary>
    /// <typeparam name="TModel">Type of the service model.</typeparam>
    public interface IAddableDataService<TModel>
    {
        /// <summary>
        /// Adds models.
        /// </summary>
        /// <param name="models">Models to be added.</param>
        /// <returns>Task</returns>
        Task<object> AddAsync(params TModel[] models);
    }
}
